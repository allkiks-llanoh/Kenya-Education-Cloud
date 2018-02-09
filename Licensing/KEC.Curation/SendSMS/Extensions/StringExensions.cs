using System.Text.RegularExpressions;


namespace KEC.Curation.SMSService.Extensions
{
    public static class StringExensions
    {
        public static bool PhoneValid(this string str)
        {
            var validationPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})";
            return Regex.IsMatch(str, validationPattern);
        }
    }
}