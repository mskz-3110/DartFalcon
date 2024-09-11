using System.Reflection;

namespace DartFalcon {
  public class Config {
    private FilePath m_FilePath;
    public FilePath FilePath {
      get => m_FilePath;
      set => m_FilePath = value;
    }

    public bool Exists(){
      return m_FilePath.Exists();
    }

    public void Set(Config config){
      foreach (FieldInfo fieldInfo in config.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)){
        fieldInfo.SetValue(this, fieldInfo.GetValue(config));
      }
    }

    public void Load(){
      Set(ConfigManager.Instance.Load(m_FilePath, GetType()));
    }

    public void Save(){
      ConfigManager.Instance.Save(this);
    }
  }
}