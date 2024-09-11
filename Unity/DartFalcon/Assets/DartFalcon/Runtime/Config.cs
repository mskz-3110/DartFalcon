using System;
using System.Reflection;

namespace DartFalcon {
  public class Config {
    [NonSerialized]
    public FilePath FilePath;

    public bool Exists(){
      return FilePath.Exists();
    }

    public void Set(Config config){
      Type type = config.GetType();
      foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)){
        fieldInfo.SetValue(this, fieldInfo.GetValue(config));
      }
      foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)){
        propertyInfo.SetValue(this, propertyInfo.GetValue(config));
      }
    }

    public void Load(){
      Set(ConfigManager.Instance.Load(FilePath, GetType()));
    }

    public void Save(){
      ConfigManager.Instance.Save(this);
    }
  }
}