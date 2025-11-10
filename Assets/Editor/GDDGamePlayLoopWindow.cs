using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GDDGamePlayLoopWindow : EditorWindow
{
   private GDDCreatorMainWindow mainWindow;
   private List<string> gamePlayLoop;
   private string loopElement;

    public static void OpenWindow(GDDCreatorMainWindow caller, List<string> gpl)
   {
      GDDGamePlayLoopWindow window = GetWindow<GDDGamePlayLoopWindow>("Gameplay Loop");
      window.mainWindow = caller;
      window.gamePlayLoop = gpl;
      window.Show();
   }

   public void OnGUI()
   {
      GUILayout.Label("Gameplay Loop Manager");
      GUILayout.Space(5);
      loopElement = GUILayout.TextField(loopElement);
      if (GUILayout.Button("Add gameplay loop element (from textbox)"))
      {
         gamePlayLoop.Add(loopElement);
      }
      GUILayout.Space(15);

      GUILayout.Label("Click the Element to Remove");
      for (int i = 0; i < gamePlayLoop.Count; i++)
      {
         if (GUILayout.Button(gamePlayLoop[i]))
         {
            gamePlayLoop.Remove(gamePlayLoop[i]);
         }
      }
      GUILayout.Space(15);
      if (GUILayout.Button("Save Gameplay Loop"))
      {
         mainWindow.addGamePlayLoop(gamePlayLoop);
         mainWindow.Repaint();
         Close();
      }
   }
}