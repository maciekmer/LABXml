namespace XmlSearch.Utils
{
    public class FileManager
    {
        private FileManager() { }
        private static FileManager _fileManager = null;

        public static FileManager GetInstance()
        {
            if (_fileManager == null)
            {
                _fileManager = new FileManager();

            }
            return _fileManager;
        }

        public File xml { get; set; }
        public File xsl { get; set; }
    }
}

