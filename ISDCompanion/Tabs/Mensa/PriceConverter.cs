using System;
using System.Globalization;
using Italbytz.Meal.Abstractions;

namespace StudyCompanion
{
    public class PriceConverter : IValueConverter
    {
        public PriceConverter()
        {
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not IPrice priceModel)
                return string.Empty;

            var price = Settings.Status switch
            {
                0 => (double?)priceModel.Students,
                1 => (double?)priceModel.Employees,
                2 => (double?)priceModel.Others,
                _ => null,
            };
            var cultureInfo = CultureInfo.GetCultureInfo("de-DE");
            return price.HasValue
                ? string.Format(cultureInfo, "{0:C}", price.Value)
                : string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
