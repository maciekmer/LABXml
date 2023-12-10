namespace XmlSearch.Utils
{
	public class File
	{
		public File(string path)
		{
			FileName = Path.GetFileName(path);
			Content = System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : throw new ArgumentException();
		}

		public File(string fileName, string content)
		{
			FileName = fileName;
			Content = content;
		}

		public string FileName { get; set; }
		public string Content { get; set; }
	}
}

