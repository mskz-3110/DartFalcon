using System;
using System.IO;
using System.Collections.Generic;

namespace DartFalcon {
  public class ConfigManager {
    static private ConfigManager s_Instance;
    static public ConfigManager Instance => s_Instance ??= new ConfigManager();

    private Dictionary<string, IConfigUtility> m_ConfigUtilities = new Dictionary<string, IConfigUtility>();

    public void SetConfigUtility(string extName, IConfigUtility configUtility){
      m_ConfigUtilities[extName] = configUtility;
    }

    public IConfigUtility GetConfigUtility(string extName){
      return m_ConfigUtilities.ContainsKey(extName) ? m_ConfigUtilities[extName] : null;
    }

    public Config Load(FilePath filePath, Type type){
      Config config = null;
      if (File.Exists(filePath.ToString()) && m_ConfigUtilities.ContainsKey(filePath.ExtName)){
        config = m_ConfigUtilities[filePath.ExtName].Load(filePath, type);
      }
      if (config == null){
        config = (Config)Activator.CreateInstance(type);
      }
      config.FilePath = filePath;
      return config;
    }

    public T Load<T>(FilePath filePath) where T : Config {
      return (T)Load(filePath, typeof(T));
    }

    public void Save(Config config){
      if (!config.FilePath.IsValid()) return;
      if (!Directory.Exists(config.FilePath.DirectoryPath)) Directory.CreateDirectory(config.FilePath.DirectoryPath);
      if (m_ConfigUtilities.ContainsKey(config.FilePath.ExtName)) m_ConfigUtilities[config.FilePath.ExtName].Save(config);
    }
  }
}