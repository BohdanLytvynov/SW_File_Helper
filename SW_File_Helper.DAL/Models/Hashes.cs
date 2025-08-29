using Newtonsoft.Json;
using SW_File_Helper.DAL.DataProviders.Favorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models
{
    public class Hashes
    {
        public string? FavoritesHash { get; set; }

        public Hashes()
        {
            FavoritesHash = string.Empty;
        }
    }
}
