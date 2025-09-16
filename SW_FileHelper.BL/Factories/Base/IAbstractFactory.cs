using SW_File_Helper.BL.Loggers.Base;

namespace SW_File_Helper.BL.Factories.Base
{
    public interface IAbstractFactory<TType>
    {
        TType Create();
    }
}
