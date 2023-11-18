using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;

public struct JugglePair
{
  public string speakingTransform;
  public string speakingImg;
  public string silentTransform;
  public string silentImg;
}


public class DialogueInterpreture : MonoBehaviour

{
    
    [SerializeField]
    public TextAsset dialogueFile;

    public GameObject dialogueDestination;

    public GameObject speakerIndicator;

    private TypeAttempt3 pairedDialogueBox;

    private ActorManager actorManager;

    string[] dialogueChunks;
    int dPos = 0;

    private Dictionary<string,JugglePair> juggleData;
    private string currentSpeaker = "";


    void Start()
    {
          pairedDialogueBox = dialogueDestination.GetComponent<TypeAttempt3>();
          pairedDialogueBox.dialogueMaster = this;
          pairedDialogueBox.curPut = "";
          dialogueChunks = dialogueFile.text.Split("\"");
          actorManager= this.GetComponent<ActorManager>();
          juggleData = new Dictionary<string,JugglePair>();
        //Debug.Log(dialogueChunks[0]);
          actorManager.GatherActors();  //Must be done before dialogue is interpreted; otherwise, it will try to animate actors that are not yet found.
          AskNext();
    }



    public void AskNext()
    {
      if(dPos < dialogueChunks.Length) //While there are chunks left , add a chunk one at a time to the dialogue box.
      {
      string next = dialogueChunks[dPos];
      Debug.Log(next);
      if(next.Contains("[")) // If this is a token chunk, parse it as such.
      {
        Debug.Log("Parsing scene");
        ParseScene(next);
        dPos += 1;
        AskNext();
      }
      else
      {
      //next = next.Replace("\n","");
      //next = next.Replace(".",".\n");
      next = next.Replace("\n","{i0}\n");
      next += "{i0}/%";
      next = "{i0}" + next;
      pairedDialogueBox.curPut = next;
      dPos += 1;
      }
      }
    }


    private void ParseScene(string Scene)
    {
      // Gathers [Key]: 'Value' from the token chunk.
      var tokens = Regex.Matches(Scene,@"\[(\w+)\]:\s*'([^']+)'");
      Debug.Log(tokens.Count);
      foreach(Match g in tokens)
      {
        //Unwraps regex matches
      string key = g.Groups[1].Value;
      string value = g.Groups[2].Value;
      //Debug.Log(key + " " + value);

      // Do something based on the key.
      switch(key)
      {
        case "Speaker":

        if(value != "None")
        {
        
        JugglePair oldSpeakerJugglePair;
        if(juggleData.TryGetValue(currentSpeaker, out oldSpeakerJugglePair)) //current speaker name is in juggleData. Have them stop talking
        {
          actorManager.DoTransform($"{currentSpeaker},{oldSpeakerJugglePair.silentTransform}");
          actorManager.SwitchImage($"{currentSpeaker},{oldSpeakerJugglePair.silentImg}");
        }
        }

        //Change the speaker name in the UI
        ChangeSpeakerName(value);
        currentSpeaker = value;

        JugglePair newSpeakerJugglePair;
        if(juggleData.TryGetValue(value,out newSpeakerJugglePair)) //New speaker name is in juggleData. Have them start talking
        {
         // Debug.Log("bingus 2");
          //JugglePair speakerJugglePair = juggleData[value];
          actorManager.DoTransform($"{value},{newSpeakerJugglePair.speakingTransform}");
          actorManager.SwitchImage($"{value},{newSpeakerJugglePair.speakingImg}");
        }

         break; //End case for Speaker tag
        case "Transform":
          actorManager.DoTransform(value);
          break;
        case "Image":
          actorManager.SwitchImage(value);
          break;
        case "Background":
          SetBackground(value);
          break;
        case "Show":
          actorManager.SpawnActor(value);
          break;
        case "Hide":
        actorManager.KillActor(value);
         break;
        case "Juggle":
          AddJuggle(value);
        break;
        case "RemoveJuggle":
          juggleData.Remove(value); 
        break;
        case "UnJuggle":
          juggleData.Clear();
        break;
      }

      }
    }

    private void ChangeSpeakerName(string name)
    {
      /*Rewrite this code later to actually change the outcome in the UI. */
      Debug.Log($"The speaker's name is changed to {name}");
      if(name == "None") {name = "" ;}
      speakerIndicator.GetComponent<TextMeshProUGUI>().text = name;
    }



    private void SetBackground(string name)
    {
      Debug.Log($"The background is changed to {name}");
      var bg = GameObject.FindGameObjectsWithTag("Background")[0].GetComponent<BackgroundBasic>();
      bg.FadeColorSwitch(Color.black,2.0f,name);
    }

    /*
    Assigns juggle behavior to an actor.
    Accepts five arguments - the name, the transform+image when talking, and the transform+image when not talking
    */
    private void AddJuggle(string Data)
    {
      string[] rawData = Data.Split(",");
      Debug.Assert(rawData.Length > 3);
      JugglePair newJugglePair;
      string actorName = rawData[0];
      Debug.Log(actorName);
      newJugglePair.speakingTransform = rawData[1];
      newJugglePair.speakingImg = rawData[2];
      newJugglePair.silentTransform = rawData[3];
      newJugglePair.silentImg = rawData[4];
      juggleData[actorName] = newJugglePair;
    }


}
