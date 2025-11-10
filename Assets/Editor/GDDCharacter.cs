using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GDDCharacter
{
   [SerializeField] private string characterName;
   [SerializeField] private string characterDescription;
   [SerializeField] private string storyImplications;
   [SerializeField] private string roles;

   public GDDCharacter(string name, string description, string story, string roles)
   {
      characterName = name;
      characterDescription = description;
      storyImplications = story;
      this.roles = roles;
   }

   // Getters
   public string getName() { return characterName; }
   public string getDescription() { return characterDescription; }
   public string getStoryImplications() { return storyImplications; }
   public string getRoles() { return roles; }


   // Setters
   public void setName(string name) { characterName = name; }
   public void setDescription(string description) { characterDescription = description; }
   public void setStory(string story) { storyImplications = story; }
   public void setRoles(string roles) { this.roles = roles; }
}