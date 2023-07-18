namespace PL.MVC.Areas.Admin.FileManager;

public class FileManagerOptions
{
	public string RootDirectoryRelativePath { get; set; }
	public int MaxFileSize { get; set; }
	public Func<string, string, bool> OnFileTypeValidate { get; set; }
}