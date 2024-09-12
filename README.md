# DartFalcon
Fast and low cost configuration reading and writing like dart

## Versions

|Version|Summary|
|:--|:--|
|1.0.0|Initial version|

## Unity
`https://github.com/mskz-3110/DartFalcon.git?path=/Unity/DartFalcon/Assets/DartFalcon/Runtime#v1.0.0`

## Dotnet
`dotnet add package DartFalcon -v 1.0.0`

## Usage
```cs
// Using namespace
using DartFalcon;

// Inherits from Config class
public class TestConfig : Config {
#if UNITY_EDITOR || UNITY_STANDALONE
  public string Name;
  public int Value;
#else
  public string Name {get; set;}
  public int Value {get; set;}
#endif
}
```
```cs
using DartFalcon;

// Set ConfigUtility corresponding to the extension
ConfigManager.Instance.SetConfigUtility("json", new JsonConfigUtility());

// Load Config
var testConfig = ConfigManager.Instance.Load<TestConfig>(new FilePath(".", "Test", "json"));

// Save Config
if (!testConfig.Exists()){
  testConfig.Name = "Test";
  testConfig.Value = 1;
  testConfig.Save();
/* Contents of written Test.json
{
  "Name": "Test",
  "Value": 1
}
*/
}
```
