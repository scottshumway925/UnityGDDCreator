using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GDDCharacterCreator : EditorWindow
{
   private GDDCreatorMainWindow mainWindow;
   private string nameInput;
   private string descriptionInput;
   private string storyImplicationsInput;
   private string rolesInput;
   private GDDCharacter editingCharacter;
   private bool isEditing;

   public static void OpenWindow(GDDCreatorMainWindow caller)
   {
      GDDCharacterCreator window = GetWindow<GDDCharacterCreator>("Character Creator");
      window.mainWindow = caller;
      window.isEditing = false;
      window.Show();
   }

   public static void OpenWindow(GDDCreatorMainWindow caller, GDDCharacter character)
   {
      GDDCharacterCreator window = GetWindow<GDDCharacterCreator> ("Character Editor");
      window.mainWindow = caller;
      window.isEditing = true;
      window.editingCharacter = character;

      window.nameInput = character.getName();
      window.descriptionInput = character.getDescription();
      window.rolesInput = character.getRoles();
      window.storyImplicationsInput = character.getStoryImplications();
      window.Show();
   }

   public void OnGUI()
   {
      GUIStyle wrapStyle = new GUIStyle(EditorStyles.textArea);
      wrapStyle.wordWrap = true;

      GUILayout.Label("Add a new Character");
      GUILayout.Space(10);

      GUILayout.Label("Character Name");
      nameInput = EditorGUILayout.TextField(nameInput);
      GUILayout.Space(10);

      GUILayout.Label("Character Description");
      descriptionInput = EditorGUILayout.TextArea(descriptionInput, wrapStyle, GUILayout.Height(100));
      GUILayout.Space(10);

      GUILayout.Label("Character Roles");
      rolesInput = EditorGUILayout.TextArea(rolesInput, wrapStyle, GUILayout.Height(100));
      GUILayout.Space(10);

      GUILayout.Label("Story Implications Offered");
      storyImplicationsInput = EditorGUILayout.TextArea(storyImplicationsInput, wrapStyle,GUILayout.Height(100));
      GUILayout.Space(10);

      if (GUILayout.Button(isEditing ? "Apply Changes" : "Save Character to Document"))
      {
         if (isEditing)
         {
            editingCharacter.setName(nameInput);
            editingCharacter.setDescription(descriptionInput);
            editingCharacter.setRoles(rolesInput);
            editingCharacter.setStory(storyImplicationsInput);
         }
         else
         {
            mainWindow.createNewCharacter(nameInput, descriptionInput, rolesInput, storyImplicationsInput);
         }
         Close();
      }

      if (isEditing)
      {
         if (GUILayout.Button("Delete Character"))
         {
            mainWindow.removeCharacter(editingCharacter);
            mainWindow.Repaint();
            Close();
         }
      }
   }
}