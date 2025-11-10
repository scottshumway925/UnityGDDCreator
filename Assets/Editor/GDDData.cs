
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GDDData
{
   [SerializeField] private string docName = "";
   [SerializeField] private string docElevatorPitch = "";
   [SerializeField] private List<string> docGenreTags = new List<string>();
   [SerializeField] private List<string> docGameplayLoop = new List<string>();
   [SerializeField] private List<GDDGameMechanic> docCoreMechanics = new List<GDDGameMechanic>();
   [SerializeField] private List<GDDCharacter> characters = new List<GDDCharacter>();
   [SerializeField] private string storyLine = "";

   // Public accessors
   public string DocName => docName;
   public string DocElevatorPitch => docElevatorPitch;
   public List<string> DocGenreTags => docGenreTags;
   public List<string> DocGameplayLoop => docGameplayLoop;
   public List<GDDGameMechanic> DocCoreMechanics => docCoreMechanics;
   public List<GDDCharacter> Characters => characters;
   public string StoryLine => storyLine;

   // Setters for updates
   public void SetDocName(string name) => docName = name;
   public void SetElevatorPitch(string pitch) => docElevatorPitch = pitch;
   public void SetGenreTags(List<string> tags) => docGenreTags = tags;
   public void SetGameplayLoop(List<string> loop) => docGameplayLoop = loop;
   public void SetCoreMechanics(List<GDDGameMechanic> mechanics) => docCoreMechanics = mechanics;
   public void SetCharacters(List<GDDCharacter> list) => characters = list;
   public void SetStoryLine(string line) => storyLine = line;
}