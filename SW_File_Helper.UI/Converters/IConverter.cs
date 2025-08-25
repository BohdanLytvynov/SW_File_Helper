using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.Converters
{
    internal interface IConverter<TSrc, TDst>
    {
        TDst Convert(TSrc src);

        TSrc ReverseConvert(TDst src);
    }
}
