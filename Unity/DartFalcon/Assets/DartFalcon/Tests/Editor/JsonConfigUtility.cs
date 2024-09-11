using System;
using System.IO;
using UnityEngine;

namespace DartFalcon {
  public class JsonConfigUtility : IConfigUtility {
    Config IConfigUtility.Load(FilePath filePath, Type type){
      return (Config)JsonUtility.FromJson(File.ReadAllText(filePath.ToString()), type);
    }

    void IConfigUtility.Save(Config config){
      File.WriteAllText(config.FilePath.ToString(), JsonUtility.ToJson(config, true));
    }
  }
}