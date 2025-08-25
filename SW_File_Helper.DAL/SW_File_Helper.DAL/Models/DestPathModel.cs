using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models
{
    public class DestPathModel : ModelBase
    {
        public string PathToFile { get; set; }

        public DestPathModel() : base()
        {
            PathToFile = string.Empty;
        }
    }
}
