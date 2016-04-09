using System;
using System.Timers;
using BankingSystem.LogicTier;
using BankingSystem.Messages;
using BankingSystem.NotificationService.Handlers;
using log4net;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.ObjectBuilder2;
using Topshelf;

namespace BankingSystem.NotificationService
{
    /// <summary>
    ///     Represents a service control which manages hub subscriptions.
    /// </summary>
    public class ProgramControl : ServiceControl
    {
        /// <summary>
        ///     The default interval. 1 minute.
        /// </summary>
        private const int DefaultInterval = 1 * 60 * 1000;
        private readonly ILog Log = LogManager.GetLogger(typeof (ProgramControl));
        private readonly IEmailService _emailService;
        private readonly ISettings _settings;
        private readonly IHandler<BalanceChangedMessage> _balanceChangedHandler;
        private IDisposable[] _disposables;
        private readonly Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramControl"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="balanceChangedHandler">The balance changed handler.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public ProgramControl(ISettings settings, IEmailService emailService, IHandler<BalanceChangedMessage> balanceChangedHandler)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (emailService == null)
                throw new ArgumentNullException(nameof(emailService));
            if (balanceChangedHandler == null)
                throw new ArgumentNullException(nameof(balanceChangedHandler));

            _settings = settings;
            _emailService = emailService;
            _balanceChangedHandler = balanceChangedHandler;

            _timer = new Timer(DefaultInterval) { AutoReset = true };
            _timer.Elapsed += OnTimerElapsed;
        }

        /// <summary>
        ///     Starts the service.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns>true if the service is started; otherwise false.</returns>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            try
            {
                var hubConnection = new HubConnection(_settings.HubUrl);
                hubConnection.TraceLevel = TraceLevels.All;
                hubConnection.TraceWriter = Console.Out;
                
                // Note: in real world we should provide this token from server.
                hubConnection.Headers.Add("SuperDuperSecretToken", "{3E663BA0-66A3-4E76-A1AE-3FB754042EDE}");

                var accountHub = hubConnection.CreateHubProxy("NotificationHub");
                _disposables = new[]
                {
                    accountHub.On<BalanceChangedMessage>("onBalanceChanged", _balanceChangedHandler.Handle)
                };
                hubConnection.Start().Wait();
                _timer.Start();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Cannot start.", ex);
                return false;
            }
        }

        /// <summary>
        ///     Stops the service.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns>true if the service is stopped; otherwise false.</returns>
        public bool Stop(HostControl hostControl)
        {
            if (_disposables != null)
            {
                _disposables.ForEach(x => x.Dispose());
                _disposables = null;
            }
            _timer.Stop();
            return true;
        }

        /// <summary>
        ///     Called when timer elapsed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();
                _emailService.DeliverEmails(_settings.SmtpSettings);
            }
            finally
            {
                _timer.Start();
            }
        }
    }
}