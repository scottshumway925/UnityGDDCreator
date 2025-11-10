using UnityEngine;
using UnityEditor;

public class GDDGameMechanicCreator : EditorWindow
{
   private GDDCreatorMainWindow mainWindow;
   private string mechanicName;
   private string mechanicDescription;
   private bool isEditing;
   private GDDGameMechanic mechanicToEdit;

    public static void OpenWindow(GDDCreatorMainWindow caller)
   {
      GDDGameMechanicCreator window = GetWindow<GDDGameMechanicCreator>("Mechanic Creator");
      window.mainWindow = caller;
      window.isEditing = false;
      window.Show();
   }

   public static void OpenWindow(GDDCreatorMainWindow caller, GDDGameMechanic mechanic)
   {
      GDDGameMechanicCreator window = GetWindow<GDDGameMechanicCreator>("Mechanic Editor");
      window.mainWindow = caller;
      window.isEditing = true;
      window.mechanicToEdit = mechanic;

      window.mechanicName = mechanic.getName();
      window.mechanicDescription = mechanic.getDescription();
      window.Show();
   }

   public void OnGUI()
   {
      GUIStyle wrapStyle = new GUIStyle(EditorStyles.textArea);
      wrapStyle.wordWrap = true;

      if (isEditing)
         GUILayout.Label("Add a New Mechanic");
      else
         GUILayout.Label("Edit Current Mechanic");
      GUILayout.Space(10);

      GUILayout.Label("Mechanic Name");
      mechanicName = EditorGUILayout.TextField(mechanicName);
      GUILayout.Space(10);

      GUILayout.Label("Mechanic Description");
      mechanicDescription = EditorGUILayout.TextArea(mechanicDescription, wrapStyle, GUILayout.Height(400));
      GUILayout.Space(10);

      if (GUILayout.Button(isEditing ? "Apply Changes" : "Save Character to Document"))
      {
         if (isEditing)
         {
            mechanicToEdit.setName(mechanicName);
            mechanicToEdit.setDescription(mechanicDescription);
         }
         else
         {
            mainWindow.createNewMechanic(mechanicName, mechanicDescription);
         }
         Close();
      }

      if (isEditing)
      {
         if (GUILayout.Button("Delete Mechanic"))
         {
            mainWindow.removeMechanic(mechanicToEdit);
            mainWindow.Repaint();
            Close();
         }
      }
   }
}
