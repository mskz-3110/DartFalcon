using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace DartFalcon {
  public class DartFalconTestEditor {
    [Test]
    public void ConfigTest(){
      ConfigManager.Instance.SetConfigUtility("json", new JsonConfigUtility());
      DartFalconTest.ConfigTest(Application.streamingAssetsPath);
      AssetDatabase.Refresh();
    }
  }
}