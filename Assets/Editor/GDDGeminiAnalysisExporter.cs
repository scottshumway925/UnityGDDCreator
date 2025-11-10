using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class GeminiResponse
{
   public Choice[] choices;
}

[System.Serializable]
public class Choice
{
   public Message message;
}

[System.Serializable]
public class Message
{
   public string content;
}

public static class GDDGeminiAnalysisExporter
{
   public static void ExportAnalysis(string rawJson)
   {
      string directory = "Assets/GameDesignDocument";
      if (!Directory.Exists(directory))
         Directory.CreateDirectory(directory);

      string filePath = Path.Combine(directory, "GDD_Analysis.txt");

      string aiOutput = ExtractContent(rawJson);
      string formatted = FormatResponse(aiOutput);

      File.WriteAllText(filePath, formatted);
      Debug.Log("AI analysis exported to: " + filePath);
      UnityEditor.AssetDatabase.Refresh();
   }

   private static string ExtractContent(string rawJson)
   {
      try
      {
         GeminiResponse response = JsonUtility.FromJson<GeminiResponse>(rawJson);
         if (response.choices != null && response.choices.Length > 0)
            return response.choices[0].message.content;
      }
      catch
      {
         Debug.LogWarning("Failed to parse Gemini response JSON, saving raw text instead.");
      }

      return rawJson; // fallback
   }

   private static string FormatResponse(string text)
   {
      if (string.IsNullOrEmpty(text)) text = "No content received from AI.";

      text = text.Replace("\\n", "\n").Replace("\\\"", "\"").Trim();

      text = WrapText(text);

      string formatted = "";
      formatted += "=========================================\n";
      formatted += "        GAME DESIGN ANALYSIS REPORT\n";
      formatted += "=========================================\n\n";

      formatted += text + "\n";

      return formatted;
   }

   private static string WrapText(string text, int maxLineLength = 130)
   {
      if (string.IsNullOrEmpty(text)) return "";

      string[] words = text.Split(' ');
      StringBuilder sb = new StringBuilder();
      int lineLength = 0;

      foreach (var word in words)
      {
         if (lineLength + word.Length + 1 > maxLineLength)
         {
            sb.AppendLine();
            lineLength = 0;
         }

         sb.Append(word + " ");
         lineLength += word.Length + 1;
      }

      return sb.ToString().TrimEnd();
   }

}
