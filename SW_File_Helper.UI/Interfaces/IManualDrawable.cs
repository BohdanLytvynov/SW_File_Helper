using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.Interfaces
{
    public interface IManualDrawable
    {
        void Draw();

        public bool ManualDraw { get; set; }
    }
}
