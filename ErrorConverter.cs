// <copyright file="ErrorConverter.cs" company="$registerdorganization$">
// Copyright (c) 2011 . All Right Reserved
// </copyright>
// <author>Leo</author>
// <email></email>
// <date>2011-07-23</date>
// <summary>A value converter for WPF and Silverlight data binding</summary>

namespace GridTest
{
    using System;
    using System.Windows.Data;
using System.Windows.Media;

    /// <summary>
    /// A Value converter
    /// </summary>
    public class ErrorConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI. 
        /// </summary>
        /// <param name="value">The source data being passed to the target </param>
        /// <param name="targetType">The Type of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the target dependency property. </returns>
        /// 
        SolidColorBrush error = new SolidColorBrush(Colors.Red);
        SolidColorBrush normal = new SolidColorBrush(Colors.Black);

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as Item;

            if (item.HasErrors)
                return error;
            else
                return normal;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object. This method is called only in TwoWay bindings. 
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The Type of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. </param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
