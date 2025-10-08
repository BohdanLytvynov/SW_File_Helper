using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SW_File_Helper.Helpers
{
    public static class StyleHelper
    {
        public static Style BuildStyle(string prop, object value)
        { 
            Style style = new Style();

            style.RegisterName(prop, value);

            return style;
        }

        public static Style BuildStyle(Dictionary<string, object> keyValuePairs)
        {
            Style style = new Style();

            foreach (var item in keyValuePairs)
            {
                style.RegisterName(item.Key, item.Value);
            }
            
            return style;
        }
    }
}
