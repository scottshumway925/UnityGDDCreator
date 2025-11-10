using UnityEngine;

[System.Serializable]
public class GDDGameMechanic
{
   [SerializeField] private string mechanicName;
   [SerializeField] private string mechanicDescription;

   public GDDGameMechanic(string name, string description)
   {
      mechanicName = name;
      mechanicDescription = description;
   }

   public string getName() { return mechanicName; }
   public string getDescription() { return mechanicDescription; }
   public void setName(string name) { mechanicName = name; }
   public void setDescription(string description) { mechanicDescription = description; }
}