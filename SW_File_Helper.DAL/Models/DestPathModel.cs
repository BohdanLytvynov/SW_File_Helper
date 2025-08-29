namespace SW_File_Helper.DAL.Models
{
    public class DestPathModel : ModelBase
    {
        public string PathToFile { get; set; }

        public DestPathModel()
        {
            PathToFile = string.Empty;

            TypeName = GetType().Name;
        }
    }
}
