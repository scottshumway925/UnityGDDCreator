using UnityEngine;
using UnityEditor;

public class GDDGenreTagsWindow : EditorWindow
{
   private GDDCreatorMainWindow mainWindow;
   private string tagString;
    public static void OpenWindow(GDDCreatorMainWindow caller)
   {
      GDDGenreTagsWindow window = GetWindow<GDDGenreTagsWindow>("Genre Tag");
      window.mainWindow = caller;
      window.Show();
   }

   public void OnGUI()
   {
      GUILayout.Label("Create a Genre Tag");
      GUILayout.Label("ie. Top-Down, 2D, Pixel-Art, etc...");
      tagString = EditorGUILayout.TextField(tagString);
      if (GUILayout.Button("Save Tag"))
      {
         mainWindow.addGenreTag(tagString);
         Close();
      }
   }
}
