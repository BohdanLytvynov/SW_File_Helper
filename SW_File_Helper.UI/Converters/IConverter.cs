namespace SW_File_Helper.Converters
{
    public interface IConverter<TSrc, TDst>
    {
        TDst Convert(TSrc src);

        TSrc ReverseConvert(TDst src);
    }
}
