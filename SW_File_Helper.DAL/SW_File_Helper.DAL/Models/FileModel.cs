using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models
{
    public class FileModel : DestPathModel
    {
        public List<string> PathToDst { get; set; }

        public FileModel() : base()
        {
            PathToDst = new List<string>();
        }
    }
}
