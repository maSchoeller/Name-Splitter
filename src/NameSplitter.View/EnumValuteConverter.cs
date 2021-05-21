using System;
using System.Globalization;
using System.Windows.Data;

using NameSplitter.Core.Models;

namespace NameSplitter.View
{
    public class EnumValuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Gender.Female => "weiblich",
                Gender.Male => "männlich",
                Gender.Neutral => "neutral",
                SalutationForm.Formal => "formell",
                SalutationForm.Informal => "informell",
                AcademicDegree.Doctor => "Doktor",
                AcademicDegree.Professor => "Professor",
                AcademicDegree.None => "keinen",
                AcademicDegree.Royal => "royal",
                Language.German => "deutsch",
                Language.English => "englisch",
                Language.Spain => "spanisch",
                Language.French => "französisch",
                object o => o?.ToString() ?? string.Empty,
                _ => "undefiniert"
            };

        }

        public string ConvertTyped(object value)
        {
            return Convert(value, null!, null!, null!) as string ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                "weiblich" => Gender.Female,
                "männlich" => Gender.Male,
                "neutral" => Gender.Neutral,
                "formel" => SalutationForm.Formal,
                "informel" => SalutationForm.Informal,
                "Doktor" => AcademicDegree.Doctor,
                "Professor" => AcademicDegree.Professor,
                "keinen" => AcademicDegree.None,
                _ => throw new InvalidCastException()
            };
        }
    }

}
