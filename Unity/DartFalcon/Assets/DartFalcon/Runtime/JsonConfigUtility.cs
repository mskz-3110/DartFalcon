using System;
using System.IO;
#if UNITY_EDITOR || UNITY_STANDALONE
using UnityEngine;
#else
using System.Text.Json;
#endif

namespace DartFalcon {
  public class JsonConfigUtility : IConfigUtility {
#if UNITY_EDITOR || UNITY_STANDALONE
#else
    static private JsonSerializerOptions Options = new JsonSerializerOptions(){
      WriteIndented = true,
    };
#endif

    Config IConfigUtility.Load(FilePath filePath, Type type){
#if UNITY_EDITOR || UNITY_STANDALONE
      return (Config)JsonUtility.FromJson(File.ReadAllText(filePath.ToString()), type);
#else
      return (Config)JsonSerializer.Deserialize(File.ReadAllText(filePath.ToString()), type);
#endif
    }

    void IConfigUtility.Save(Config config){
#if UNITY_EDITOR || UNITY_STANDALONE
      File.WriteAllText(config.FilePath.ToString(), JsonUtility.ToJson(config, true));
#else
      File.WriteAllText(config.FilePath.ToString(), JsonSerializer.Serialize(config, config.GetType(), Options));
#endif
    }
  }
}