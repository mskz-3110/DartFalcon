using System;
using System.IO;
#if UNITY_EDITOR || UNITY_STANDALONE
using UnityEngine;
#else
using System.Text.Json;
#endif

namespace DartFalcon {
  public class JsonConfigUtility : IConfigUtility {
    private bool m_PrettyPrint = true;
    public bool PrettyPrint {
      get => m_PrettyPrint;
      set {
        m_PrettyPrint = value;
#if UNITY_EDITOR || UNITY_STANDALONE
#else
        m_Options.WriteIndented = m_PrettyPrint;
#endif
      }
    }

#if UNITY_EDITOR || UNITY_STANDALONE
#else
    private JsonSerializerOptions m_Options = new JsonSerializerOptions();
#endif

    public JsonConfigUtility(bool prettyPrint = true){
      PrettyPrint = prettyPrint;
    }

    Config IConfigUtility.Load(FilePath filePath, Type type){
#if UNITY_EDITOR || UNITY_STANDALONE
      return (Config)JsonUtility.FromJson(File.ReadAllText(filePath.ToString()), type);
#else
      return (Config)JsonSerializer.Deserialize(File.ReadAllText(filePath.ToString()), type);
#endif
    }

    void IConfigUtility.Save(Config config){
#if UNITY_EDITOR || UNITY_STANDALONE
      File.WriteAllText(config.FilePath.ToString(), JsonUtility.ToJson(config, m_PrettyPrint));
#else
      File.WriteAllText(config.FilePath.ToString(), JsonSerializer.Serialize(config, config.GetType(), m_Options));
#endif
    }
  }
}