using System;

namespace DartFalcon {
  public interface IConfigUtility {
    Config Load(FilePath filePath, Type type);

    void Save(Config config);
  }
}