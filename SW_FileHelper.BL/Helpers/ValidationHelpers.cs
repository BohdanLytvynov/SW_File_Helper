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
    }
}
