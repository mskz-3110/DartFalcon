using System.Diagnostics;

namespace DartFalcon {
  public class DartFalconTest {
    public class EnvironmentConfig : Config {
#if UNITY_EDITOR || UNITY_STANDALONE
      public string LocalConfigPath;
#else
      public string LocalConfigPath {get; set;}
#endif
    }

    public class DeviceConfig : Config {
#if UNITY_EDITOR || UNITY_STANDALONE
      public string Name;
#else
      public string Name {get; set;}
#endif
    }

    static public void ConfigTest(string rootPath){
      var environmentConfig = ConfigManager.Instance.Load<EnvironmentConfig>(new FilePath($"{rootPath}/GlobalConfig", "Environment", "json"));
      if (!environmentConfig.Exists()){
        environmentConfig.LocalConfigPath = "../LocalConfig";
        environmentConfig.Save();
        environmentConfig.LocalConfigPath = "";
      }
      environmentConfig.Load();
      Assert(environmentConfig.LocalConfigPath == "../LocalConfig");
      Assert(environmentConfig.FilePath.FileName == "Environment");
      Assert(environmentConfig.FilePath.ExtName == "json");

      var deviceConfig = ConfigManager.Instance.Load<DeviceConfig>(new FilePath($"{environmentConfig.FilePath.DirectoryPath}/{environmentConfig.LocalConfigPath}", "DeviceA", "json"));
      if (!deviceConfig.Exists()){
        deviceConfig.Name = "A";
        deviceConfig.Save();
        deviceConfig.Name = "";
      }
      deviceConfig.Load();
      Assert(deviceConfig.Name == "A");
      Assert(deviceConfig.FilePath.FileName == "DeviceA");
      Assert(deviceConfig.FilePath.ExtName == "json");
      deviceConfig.FilePath.FileName = "DeviceB";
      if (!deviceConfig.Exists()){
        deviceConfig.Name = "B";
        deviceConfig.Save();
        deviceConfig.Name = "";
      }
      deviceConfig.Load();
      Assert(deviceConfig.Name == "B");
      Assert(deviceConfig.FilePath.FileName == "DeviceB");
      Assert(deviceConfig.FilePath.ExtName == "json");
    }

    static private void Assert(bool condition){
      if (!condition) LogError($"Assert error: {(new StackTrace(true)).GetFrame(1).GetFileLineNumber()}");
    }

    static private void LogError(string message){
#if UNITY_EDITOR || UNITY_STANDALONE
      UnityEngine.Debug.LogError(message);
#else
      System.Console.WriteLine(message);
#endif
    }
  }
}