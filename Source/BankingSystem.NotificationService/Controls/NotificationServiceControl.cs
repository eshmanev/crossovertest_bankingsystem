using System.Timers;
using Topshelf;

namespace BankingSystem.NotificationService.Controls
{
    /// <summary>
    ///     Represents a service control which manages notifications timeouts.
    /// </summary>
    /// <seealso cref="Topshelf.ServiceControl" />
    public class NotificationServiceControl : ServiceControl
    {
        /// <summary>
        ///     The default interval. 15 minutes.
        /// </summary>
        private const int DefaultInterval = 15*60*1000;

        private readonly Timer _timer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotificationServiceControl" /> class.
        /// </summary>
        public NotificationServiceControl()
        {
            _timer = new Timer(DefaultInterval) {AutoReset = true};
            _timer.Elapsed += OnTimerElapsed;
        }

        /// <summary>
        ///     Starts the service.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns>true if the service is started; otherwise false.</returns>
        public bool Start(HostControl hostControl)
        {
            _timer.Start();
            return true;
        }

        /// <summary>
        ///     Stops the service.
        /// </summary>
        /// <param name="hostControl">The host control.</param>
        /// <returns>true if the service is stopped; otherwise false.</returns>
        public bool Stop(HostControl hostControl)
        {
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
        }
    }
}