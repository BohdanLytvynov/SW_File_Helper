using SW_File_Helper.BL.Builders.Base;

namespace SW_File_Helper.BL.Directors.Base
{
    public interface IDirector<TBuilder, TObject>
       where TBuilder : IBuilder<TObject>
    {
        public TBuilder Builder { get; init; }
    }
}
