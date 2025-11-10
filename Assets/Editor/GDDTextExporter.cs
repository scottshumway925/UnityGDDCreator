using UnityEngine;
using System.IO;
using System.Text;
using System;

public class GDDTextExporter
{
   private static readonly string exportFolder = "Assets/GameDesignDocument";
   private static readonly string exportFileName = "GameDesignDocument.txt";

   private static string FullPath => Path.Combine(exportFolder, exportFileName);

   public static void ExportToText(GDDData gddData)
   {
      if (!Directory.Exists(exportFolder))
         Directory.CreateDirectory(exportFolder);

      int wrapWidth = 125;

      StringBuilder sb = new StringBuilder();

      sb.AppendLine($"=== {gddData.DocName} ===\n");

      if (gddData.DocGenreTags.Count > 0)
      {
         sb.Append("Genres: ");
         foreach (string tag in gddData.DocGenreTags)
         {
            sb.Append($"{tag} | ");
         }
         sb.Length -= 3;
         sb.AppendLine("\n");
      }
      else
      {
         sb.AppendLine("No genre tags defined.\n");
      }

      sb.AppendLine("=== GAME OVERVIEW ===");
      AppendIndented(sb, gddData.DocElevatorPitch, 2, wrapWidth);
      sb.AppendLine();

      sb.AppendLine("=== STORY OVERVIEW ===");
      AppendIndented(sb, gddData.StoryLine, 2, wrapWidth);
      sb.AppendLine();

      sb.AppendLine("=== GAMEPLAY LOOP ===");
      if (gddData.DocGameplayLoop.Count > 0)
      {
         for (int i = 0; i < gddData.DocGameplayLoop.Count; i++)
         {
            AppendIndented(sb, $"{i + 1}. {gddData.DocGameplayLoop[i]}", 4, wrapWidth);
         }
      }
      else
      {
         AppendIndented(sb, "(No gameplay loop defined)", 4, wrapWidth);
      }
      sb.AppendLine();

      sb.AppendLine("=== CHARACTERS ===");
      if (gddData.Characters.Count > 0)
      {
         foreach (GDDCharacter character in gddData.Characters)
         {
            sb.AppendLine($"  - {character.getName()} -");
            sb.AppendLine("    Description: ");
            AppendIndented(sb, character.getDescription(), 6, wrapWidth);
            sb.AppendLine("    Roles: ");
            AppendIndented(sb, character.getRoles(), 6, wrapWidth);
            sb.AppendLine("    Story Impact: ");
            AppendIndented(sb, character.getStoryImplications(), 6, wrapWidth);
            sb.AppendLine();
         }
      }
      else
      {
         AppendIndented(sb, "No characters defined.", 4, wrapWidth);
         sb.AppendLine();
      }

      sb.AppendLine("=== GAME MECHANICS ===");
      if (gddData.DocCoreMechanics.Count > 0)
      {
         foreach (GDDGameMechanic mechanic in gddData.DocCoreMechanics)
         {
            sb.AppendLine($"  - {mechanic.getName()} -");
            AppendIndented(sb, mechanic.getDescription(), 6, wrapWidth);
            sb.AppendLine();
         }
      }
      else
      {
         AppendIndented(sb, "No mechanics defined.", 4, wrapWidth);
         sb.AppendLine();
      }

      File.WriteAllText(FullPath, sb.ToString());
      Debug.Log($"GDD exported as text file at: {FullPath}");
      UnityEditor.AssetDatabase.Refresh();
   }

   /***************************
   * Helper Functions
   ****************************/

   private static void AppendIndented(StringBuilder sb, string text, int indent = 2, int wrapWidth = 80)
   {
      if (string.IsNullOrWhiteSpace(text))
      {
         sb.AppendLine(new string(' ', indent) + "(No content)");
         return;
      }

      string[] words = text.Split(' ');
      int lineLength = 0;
      sb.Append(new string(' ', indent));

      foreach (string word in words)
      {
         if (lineLength + word.Length + 1 > wrapWidth)
         {
            sb.AppendLine();
            sb.Append(new string(' ', indent));
            lineLength = 0;
         }

         sb.Append(word + " ");
         lineLength += word.Length + 1;
      }
      sb.AppendLine();
   }
}
