using System;

namespace BankingSystem.IntegrationTests
{
    public class TestVars
    {
        /// <summary>
        ///     The _port number
        /// </summary>
        private static int _portNumber = 1234;

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

        /// <summary>
        ///     Allocates a port number.
        /// </summary>
        public static int AllocPortNumber()
        {
            return ++_portNumber;
        }
    }
}