using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models
{
    public class IPAddressFavorites : ModelBase
    {
        public IPAddressFavorites()
        {
            TypeName = this.GetType().Name;
        }

        public string IPAddress { get; set; }

        public override string ToString()
        {
            return IPAddress;
        }
    }
}
