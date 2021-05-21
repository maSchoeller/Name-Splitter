using System.Linq;

using NameSplitter.Core.Models;

namespace NameSplitter.Core
{
    public class SalutationGenerator
    {

        public string GenerateSalutation(NameSplittingResult result)
        {
            var resultstring = string.Empty;
            if (result.Salutation is null)
            {
                resultstring += "Hallo ";
            }
            else
            {
                resultstring += (result.Salutation.Gender, result.Salutation.Language) switch
                {
                    (Gender.Male, Language.German) => "Sehr geehrter Herr ",
                    (Gender.Female, Language.German) => "Sehr geehrte Frau ",
                    (Gender.Male, Language.Spain) => "Señor ",
                    (Gender.Female, Language.Spain) => "Señora ",
                    (Gender.Male, Language.English) => "Mr. ",
                    (Gender.Female, Language.English) => "Mrs. ",
                    (Gender.Male, Language.French) => "Mme. ",
                    (Gender.Female, Language.French) => "M. ",
                    _ => ""
                };
            }

            var deg = result.Titles.OrderByDescending(t => (int)t.Degree).FirstOrDefault();
            if (deg is not null)
            {
                resultstring += deg.Degree switch
                {
                    AcademicDegree.Doctor => "Dr. ",
                    AcademicDegree.Professor => "Prof. ",
                    _ => "",
                };
            }
            resultstring += result.Firstname + " " + result.Lastname;
            return resultstring;
        }
    }
}
