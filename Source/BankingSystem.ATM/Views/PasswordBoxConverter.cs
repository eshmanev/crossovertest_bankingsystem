using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BankingSystem.ATM.Views
{
    /// <summary>
    ///     Represents a converter which extracts a password from non-bindable PasswordBox.
    /// </summary>
    public class PasswordBoxConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new PasswordBoxWrapper((PasswordBox) value);
        }

        /// <summary>
        ///     Not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Defines a wrapper of the non-bindable password box.
        /// </summary>
        public class PasswordBoxWrapper : IWrappedValue<string>
        {
            private readonly PasswordBox _source;

            /// <summary>
            ///     Initializes a new instance of the <see cref="PasswordBoxWrapper" /> class.
            /// </summary>
            /// <param name="source">The source.</param>
            public PasswordBoxWrapper(PasswordBox source)
            {
                _source = source;
            }

            /// <summary>
            ///     Gets the value.
            /// </summary>
            /// <value>
            ///     The value.
            /// </value>
            public string Value
            {
                get { return _source.Password; }
                set { _source.Password = value; }  
            } 
        }
    }
}