using SW_File_Helper.BL.Factories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Builders.Base
{
    public interface IBuilder<TType>
    {
        IAbstractFactory<TType> Factory { get; init; }
        TType GetObject();
    }
}
