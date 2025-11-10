using UnityEngine;
using System.IO;

public static class GDDDataManager
{
   private static readonly string SaveFolder = "Assets/Editor/GDDData";
   private static readonly string SaveFileName = "CurrentGDD.json";

   private static string FullPath => Path.Combine(SaveFolder, SaveFileName);

   public static void Save(GDDData data)
   {
      if (!Directory.Exists(SaveFolder))
         Directory.CreateDirectory(SaveFolder);

      string json = JsonUtility.ToJson(data, true);
      File.WriteAllText(FullPath, json);
      Debug.Log($"GDD data saved to {FullPath}");
   }

   public static GDDData Load()
   {
      if (!File.Exists(FullPath))
      {
         Debug.Log("No existing GDD file found. Creating new blank document.");
         return new GDDData();
      }

      string json = File.ReadAllText(FullPath);
      GDDData data = JsonUtility.FromJson<GDDData>(json);
      Debug.Log($"Loaded GDD data from {FullPath}");
      return data ?? new GDDData();
   }
}
