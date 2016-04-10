using System;

namespace BankingSystem.IntegrationTests
{
    public class TestVars
    {
        /// <summary>
        ///     The base URL of the test Web API server.
        /// </summary>
        public const string WebApiBaseUrl = "http://localhost:1234";

        /// <summary>
        ///     The base URL of the test SignalR server.
        /// </summary>
        public const string SignalRBaseUrl = "http://localhost:1235";

        /// <summary>
        ///     The valid bankcard number
        /// </summary>
        public const string ValidCardNumber = "2111222233335555";

        /// <summary>
        ///     The valid bankcard currency
        /// </summary>
        public const string ValidCardCurrency = "JPY";

        /// <summary>
        ///     The valid bankcard PIN code
        /// </summary>
        public const string ValidPinCode = "0000";

        /// <summary>
        ///     The default timeout
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(3);
    }
}