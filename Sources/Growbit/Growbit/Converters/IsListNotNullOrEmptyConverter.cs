using System.Globalization;

namespace Growbit.Converters;

public class IsListNotNullOrEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            return count > 0; 
        }

        return Binding.DoNothing; // Or a default false/Visibility.Collapsed
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}