namespace SW_File_Helper.DAL.Models
{
    public class FileModel : DestPathModel
    {
        public List<string> PathToDst { get; set; }

        public FileModel()
        {
            PathToDst = new List<string>();

            TypeName = GetType().Name;
        }
    }
}
