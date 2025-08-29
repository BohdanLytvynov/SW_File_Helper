namespace SW_File_Helper.DAL.Helpers
{
    public static class IOHelper
    {
        public static bool IsFileExists(string path)
        {
            CheckPathIsNull(path);
            return File.Exists(path);
        }

        public static bool IsDirectoryExists(string path)
        {
            CheckPathIsNull(path);
            return Directory.Exists(path);
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            CheckPathIsNull(path);
            if(!IsDirectoryExists(path))
                Directory.CreateDirectory(path);
        }

        public static void CreateFileIfNotExists(string path)
        {
            CheckPathIsNull(path);
            if (!IsFileExists(path))
            {
                var fs = File.Create(path);
                fs.Close();
                fs.Dispose();
            }
            
        }

        public static string ReadAll(string path)
        { 
            CheckPathIsNull(path);
            return File.ReadAllText(path);
        }

        public static void WriteAll(string path, string data)
        {
            CheckPathIsNull(path);
            if (data == null) throw new ArgumentNullException("data");

            File.WriteAllText(path, data);
        }

        public static void RenameFile(string pathToDir, string fileName, string newFileName)
        {
            if(string.IsNullOrEmpty(newFileName)) 
                throw new ArgumentNullException(nameof(newFileName));

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var pathToFileOld = pathToDir + Path.DirectorySeparatorChar + fileName;

            if (!IsFileExists(pathToFileOld))
                return;

            var pathToFileNew = pathToDir + Path.DirectorySeparatorChar + newFileName;

            if (IsFileExists(pathToFileNew))
                return;

            File.Move(pathToFileOld, pathToFileNew);
        }

        public static void Copy(string srcPath, string destPath)
        { 
            if(string.IsNullOrEmpty(srcPath))
                throw new ArgumentNullException(nameof(srcPath));

            if(string.IsNullOrEmpty(destPath))
                throw new ArgumentNullException(nameof(destPath));

            File.Copy(srcPath, destPath);
        }

        private static void CheckPathIsNull(string path)
        { 
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
        }
    }
}
