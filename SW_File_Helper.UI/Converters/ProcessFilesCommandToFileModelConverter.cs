using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Models.TCPModels;

namespace SW_File_Helper.Converters
{
    public class ProcessFilesCommandToFileModelConverter : IProcessFilesCommandToFileModelConverter
    {
        public FileModel Convert(ProcessFilesCommand src)
        {
            List<string> destinations = new List<string>();
            foreach (var item in src.Dest)
            {
                destinations.Add(item);
            }

            return new FileModel()
            {
                PathToFile = src.Src,
                PathToDst = destinations
            };
        }

        public ProcessFilesCommand ReverseConvert(FileModel src)
        {
            List<string> destinations = new List<string>();
            foreach (var item in src.PathToDst)
            {
                destinations.Add(item);
            }
            return new ProcessFilesCommand()
            {
                Src = src.PathToFile,
                Dest = destinations
            };

        }
    }
}
