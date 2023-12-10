namespace XmlSearch.Utils
{
	public static class FilePickerOptions
	{
        public static PickOptions GetXmlPickOptions()
        {
            var xmlFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new string[] { "application/xml" } },
                    { DevicePlatform.iOS, new string[] { "com.adobe.xml" } },
                    { DevicePlatform.macOS, new string[] { "xml" } },
                    { DevicePlatform.MacCatalyst, new string[] { "xml" } },
                    { DevicePlatform.WinUI, new string[] { ".xml" } },
                });
            return new PickOptions
            {
                FileTypes = xmlFileType
            }; 
        }

        public static PickOptions GetXslPickOptions()
        {
            var xslFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new string[] { "application/xsl" } },
                    { DevicePlatform.iOS, new string[] { "com.adobe.xsl" } },
                    { DevicePlatform.macOS, new string[] { "xsl" } },
                    { DevicePlatform.MacCatalyst, new string[] { "xsl" } },
                    { DevicePlatform.WinUI, new string[] { ".xsl" } },
                });
            return new PickOptions
            {
                FileTypes = xslFileType
            };
        }
    }
}

