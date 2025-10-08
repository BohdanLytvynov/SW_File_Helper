using System.Net;
using System.Text.RegularExpressions;

namespace SW_File_Helper.BL.Helpers
{
    public static class ValidationHelpers
    {
        private static Regex pathValidationRegex = new Regex("/^[a-zA-Z]:\\(?:\\w+\\?)*$/");

        public static bool IsTextEmpty(string text, out string error)
        {
            if (string.IsNullOrEmpty(text)) 
            {
                error = "Field is Empty!";
                return true;
            }
            error = string.Empty;
            return false;
        }

        public static bool IsPathValid(string path, out string error)
        { 
            if(IsTextEmpty(path, out error)) return false;

            //if (!pathValidationRegex.IsMatch(path))
            //{
            //    error = "Incorrect path!";
            //    return false;
            //}

            return true;
        }

        public static bool IsIPAddressValid(string value, out string error)
        { 
            if(IsTextEmpty(value, out error)) return false;
            IPAddress iPAddress_;

            if (!IPAddress.TryParse(value, out iPAddress_))
            {
                error = "Invalid input!";
                return false;
            }

            return true;
        }

        public static bool IsIntegerNumberValid(string value, out string error)
        {
            if (IsTextEmpty(value, out error)) return false;

            if (!int.TryParse(value, out int res_))
            {
                error = "Invalid input!";
                return false;
            }

            return true;
        }
    }
}
