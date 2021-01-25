using System.Text.RegularExpressions;

namespace PrisonRP.GameMode.Utilities
{
    public static class InputValidation
    {
        public static bool IsInputValidAge(string number)
        {
            var regex = new Regex(@"^[0-9]+$", RegexOptions.Compiled);

            if (regex.IsMatch(number))
            {
                return int.Parse(number) >= 18 && int.Parse(number) <= 70;
            }

            return false;
        }
    }
}