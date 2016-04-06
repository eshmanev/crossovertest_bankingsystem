using System;
using BankingSystem.Common.Messages;
using BankingSystem.NotificationService.Handlers;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Practices.ObjectBuilder2;
using Topshelf;

namespace BankingSystem.NotificationService.Controls
{
    /// <summary>
    ///     Represents a service control which manages hub subscriptions.
    /// </summary>
    public class HubServiceControl : ServiceControl
    {
        private readonly ISettings _settings;
        private readonly IHandler<BalanceChangedMessage> _balanceChangedHandler;
        private IDisposable[] _disposables;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HubServiceControl" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="balanceChangedHandler">The balance changed handler.</param>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public HubServiceControl(ISettings settings, IHandler<BalanceChangedMessage> balanceChangedHandler)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (balanceChangedHandler == null)
                throw new ArgumentNullException(nameof(balanceChangedHandler));

            _settings = settings;
            _balanceChangedHandler = balanceChangedHandler;
        }

        /// <summary>
        ///     Starts the service.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns>true if the service is started; otherwise false.</returns>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            var hubConnection = new HubConnection(_settings.HubUrl);
            hubConnection.TraceLevel = TraceLevels.All;
            hubConnection.TraceWriter = Console.Out;
            var accountHub = hubConnection.CreateHubProxy("AccountHub");
            _disposables = new[]
            {
                accountHub.On<BalanceChangedMessage>("onBalanceChanged", _balanceChangedHandler.Handle)
            };
            hubConnection.Start().Wait();
            return true;
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
            return true;
        }
    }
}