using System.IO;
using System.Text;

namespace DartFalcon {
  public class FilePath {
    static public FilePath Create(string filePath){
      return new FilePath(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath), Path.GetExtension(filePath).Remove(0, 1));
    }

    public string DirectoryPath;

    public string FileName;

    public string ExtName;

    private StringBuilder m_StringBuilder = new StringBuilder();

    public FilePath(string directoryPath, string fileName, string extName){
      DirectoryPath = directoryPath;
      FileName = fileName;
      ExtName = extName;
    }

    public FilePath(string fileName, string extName) : this(".", fileName, extName){}

    public FilePath() : this(".", "", ""){}

    public bool IsValid(){
      if (string.IsNullOrEmpty(DirectoryPath)) return false;
      if (string.IsNullOrEmpty(FileName)) return false;
      if (string.IsNullOrEmpty(ExtName)) return false;
      return true;
    }

    public bool Exists(){
      return IsValid() && File.Exists(ToString());
    }

    public override string ToString(){
      m_StringBuilder.Clear();
      m_StringBuilder.Append(DirectoryPath);
      m_StringBuilder.Append("/");
      m_StringBuilder.Append(FileName);
      m_StringBuilder.Append(".");
      m_StringBuilder.Append(ExtName);
      return m_StringBuilder.ToString();
    }
  }
}