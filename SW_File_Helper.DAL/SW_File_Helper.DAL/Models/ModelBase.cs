using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models
{
    public class ModelBase
    {
        public int Id { get; set; }
        
        public ModelBase()
        {
            Id = -1;
        }
    }
}
