using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Unity.Plastic.Newtonsoft.Json.Linq;

public class GDDCreatorMainWindow : EditorWindow
{
   // Overall Variables
   private int sectionSpacing = 25;
   private int subSectionSpacing = 10;
   private GDDData gddData;

   private Vector2 mainScroll;
   private Vector2 tagScroll;
   private Vector2 characterScroll;
   private Vector2 mechanicsScroll;

   // APIKey Handling
   private bool showApiKey = false;
   private string apiKey = "";

[MenuItem("Window/Game Design Doc Creator")]
    public static void OpenWindow()
   {
      GetWindow<GDDCreatorMainWindow>("GDD Creator");
   }

   private void OnEnable()
   {
      gddData = GDDDataManager.Load();
   }

   private void OnDisable()
   {
      GDDDataManager.Save(gddData);
   }

   public void OnGUI()
   {
      if (gddData == null)
         gddData = new GDDData();

      /**********************************************
       * Setting the basic GUI settings
       **********************************************/
      GUIStyle wrapStyle = new GUIStyle(EditorStyles.textArea);
      wrapStyle.wordWrap = true;
      mainScroll = EditorGUILayout.BeginScrollView(mainScroll);

      /**********************************************
       * Handling the game title
       **********************************************/
      GUILayout.Label("Add Game Title");
      gddData.SetDocName(EditorGUILayout.TextField(gddData.DocName));
      GUILayout.Space(sectionSpacing);

      /**********************************************
       * Handling the genre tags
       **********************************************/
      GUILayout.Label("Manage Genre Tags");
      if (GUILayout.Button("Add a Tag"))
      {
         GDDGenreTagsWindow.OpenWindow(this);
      }

      GUILayout.Space(5);

      GUILayout.Label("Click a Tag to Remove");
      for (int i = 0; i < gddData.DocGenreTags.Count; i++)
      {
         if (GUILayout.Button(gddData.DocGenreTags[i]))
         {
            gddData.DocGenreTags.Remove(gddData.DocGenreTags[i]);
         }
      }

      GUILayout.Space(sectionSpacing);

      /**********************************************
       * Handles the elevator pitch / breif 
       * description for the game
       **********************************************/
      GUILayout.Label("Add an Elevator Pitch");
      float elevatorTextHeight = wrapStyle.CalcHeight(new GUIContent(gddData.DocElevatorPitch), EditorGUIUtility.currentViewWidth - 22f);
      elevatorTextHeight = Mathf.Max(100f, elevatorTextHeight);
      gddData.SetElevatorPitch(EditorGUILayout.TextArea(gddData.DocElevatorPitch, wrapStyle, GUILayout.Height(elevatorTextHeight)));
      GUILayout.Space(sectionSpacing);

      /**********************************************
       * Handles creating new and editing existing
       * gameplay loop
       **********************************************/
      GUILayout.Label("Manage Your Game Loop");
      GUILayout.Space(5);

      GUILayout.Label("Current Loop");
      for (int i = 0; i < gddData.DocGameplayLoop.Count; i++)
      {
         GUILayout.Label((i + 1) + ". " + gddData.DocGameplayLoop[i]);
      }
      GUILayout.Space(5);

      if (GUILayout.Button("Manage Gameplay Loop"))
      {
         GDDGamePlayLoopWindow.OpenWindow(this, gddData.DocGameplayLoop);
      }
      GUILayout.Space(sectionSpacing);

      /**********************************************
       * Handles creating new and editing existing
       * characters in the gdd
       **********************************************/
      GUILayout.Label("Manage Characters");
      if (GUILayout.Button("Create New Character"))
      {
         GDDCharacterCreator.OpenWindow(this);
      }

      GUILayout.Space(5);

      GUILayout.Label("Click character to edit: ");
      for (int i = 0; i < gddData.Characters.Count; i++)
      {
         if (GUILayout.Button(gddData.Characters[i].getName()))
         {
            GDDCharacterCreator.OpenWindow(this, gddData.Characters[i]);
         }
      }

      EditorGUILayout.Space(sectionSpacing);

      /**********************************************
       * Handles the games mechanics
       **********************************************/

      GUILayout.Label("Manage Game Mechanics");
      if (GUILayout.Button("Create New Mechanic"))
      {
         GDDGameMechanicCreator.OpenWindow(this);
      }

      GUILayout.Space(5);

      GUILayout.Label("Click mechanic to edit: ");
      for (int i = 0; i < gddData.DocCoreMechanics.Count; i++)
      {
         if (GUILayout.Button(gddData.DocCoreMechanics[i].getName()))
         {
            GDDGameMechanicCreator.OpenWindow(this, gddData.DocCoreMechanics[i]);
         }
      }

      EditorGUILayout.Space(sectionSpacing);

      /**********************************************
       * Handles the games story
       **********************************************/

      GUILayout.Label("Add Story Overview");
      float storyTextHeight = wrapStyle.CalcHeight(new GUIContent(gddData.StoryLine), EditorGUIUtility.currentViewWidth - 22f);
      storyTextHeight = Mathf.Max(100f, storyTextHeight);
      gddData.SetStoryLine(EditorGUILayout.TextArea(gddData.StoryLine, wrapStyle, GUILayout.Height(storyTextHeight)));
      GUILayout.Space(sectionSpacing);

      /**********************************************
       * IO Functions
       **********************************************/

      showApiKey = EditorGUILayout.Foldout(showApiKey, "OpenRouter API Key");

      if (showApiKey)
      {
         EditorGUILayout.HelpBox("Enter your OpenRouter API key here. It will be hidden when typing.", MessageType.Info);
         apiKey = EditorGUILayout.PasswordField("API Key", apiKey);
      }

      GUILayout.Space(subSectionSpacing);

      if (GUILayout.Button("Export GDD as Text File", GUILayout.Height(30)))
      {
         GDDTextExporter.ExportToText(gddData);
      }

      GUILayout.Space(subSectionSpacing);

      if (GUILayout.Button("Get AI Analysis", GUILayout.Height(30)))
      {
         AnalyzeWithGemini();
      }


      EditorGUILayout.EndScrollView();
   }

   // CharacterList Methods
   public void createNewCharacter(string name, string description, string roles, string story)
   {
      GDDCharacter newCharacter = new GDDCharacter(name, description, roles, story);
      gddData.Characters.Add(newCharacter);
   }

   public void removeCharacter(GDDCharacter character)
   {
      gddData.Characters.Remove(character);
   }

   // Genre Tags Methods
   public void addGenreTag(string tag)
   {
      gddData.DocGenreTags.Add(tag);
   }

   // Gameplay Loop Methods
   public void addGamePlayLoop(List<string> gpl)
   {
      gddData.SetGameplayLoop(gpl);
   }

   public void createNewMechanic(string name, string description)
   {
      GDDGameMechanic mechanic = new GDDGameMechanic(name, description);
      gddData.DocCoreMechanics.Add(mechanic);
   }

   public void removeMechanic(GDDGameMechanic mechanic)
   {
      gddData.DocCoreMechanics.Remove(mechanic);
   }

   // Getting AI Analysis
   private async void AnalyzeWithGemini()
   {
      string filePath = "Assets/GameDesignDocument/GameDesignDocument.txt";

      if (!File.Exists(filePath))
      {
         Debug.LogError("Could not find Game Design Document. Please export the document and try again.");
         return;
      }

      string fileContent = File.ReadAllText(filePath);

      if (string.IsNullOrEmpty(apiKey))
      {
         Debug.LogError("Please enter your API key in the OpenRouter API Key section.");
         return;
      }

      string prompt =
         "You are a game designer. Please analyze this game design document. " +
         "I would like multiple sections in regards to your analysis. " +
         "1 - What are the strengths of the project. " +
         "2 - What are the weaknesses of the project. " +
         "3 - What are the market considerations of the project. " +
         "4 - What are the recommendations you would make to help ensure the success of this game. " +
         "Do not mention anything about needing graphical representations in the GDD, " +
         "as it will be purely text based:\n\n" + fileContent;

      string jsonBody = $@"{{
         ""model"": ""google/gemini-2.0-flash-exp:free"",
         ""messages"": [
            {{
               ""role"": ""user"",
               ""content"": [
                  {{
                     ""type"": ""text"",
                     ""text"": ""{EscapeJson(prompt)}""
                  }}
               ]
            }}
         ]
      }}";

      using (UnityWebRequest request = new UnityWebRequest("https://openrouter.ai/api/v1/chat/completions", "POST"))
      {
         byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
         request.downloadHandler = new DownloadHandlerBuffer();
         request.SetRequestHeader("Content-Type", "application/json");
         request.SetRequestHeader("Authorization", "Bearer " + apiKey);

         Debug.Log("Sending request to Gemini...");
         var async = request.SendWebRequest();
         while (!async.isDone)
            await Task.Yield();

         if (request.result == UnityWebRequest.Result.Success)
         {
            string responseText = request.downloadHandler.text;
            Debug.Log("Gemini Response Received!");
            GDDGeminiAnalysisExporter.ExportAnalysis(responseText);
         }
         else
         {
            Debug.LogError("Gemini request failed: " + request.error + "\n" + request.downloadHandler.text);
         }
      }
   }

   // Extract clean text response

   private string EscapeJson(string text)
   {
      return text.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "");
   }
}