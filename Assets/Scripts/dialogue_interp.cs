using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

public class dialogue_interp : MonoBehaviour

{
    
    [SerializeField]
    public TextAsset dialogueFile;

    public GameObject dialogueDestination;

    private type_attempt_3 pairedDialogueBox;

    string[] dialogueChunks;
    int d_pos = 0;


    void Start()
    {
          pairedDialogueBox = dialogueDestination.GetComponent<type_attempt_3>();
          pairedDialogueBox.dialoguemaster = this;
          pairedDialogueBox.curPut = "";
          dialogueChunks = dialogueFile.text.Split("\"");
          //Debug.Log(dialogueChunks[0]);
          askNext();
    }


    public void askNext()
    {
      if(d_pos < dialogueChunks.Length) //While there are chunks left , add a chunk one at a time to the dialogue box.
      {
      string next = dialogueChunks[d_pos];
      Debug.Log(next);
      if(next.Contains("[")) // If this is a token chunk, parse it as such.
      {
        Debug.Log("Parsing scene");
        parseScene(next);
        d_pos += 1;
        askNext();
      }
      else
      {
      next = next.Replace("\n","{i0}\n");
      next += "{i0}/%";
      next = "{i0}" + next;
      pairedDialogueBox.curPut = next;
      d_pos += 1;
      }
      }
    }


    private void parseScene(string Scene)
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
         changeSpeakerName(value);
         break;
        case "Location":
         setLocation(value);
         break;
      }

      }
    }

    private void changeSpeakerName(string Name)
    {
      /*Rewrite this code later to actually change the outcome in the UI. */
      Debug.Log($"The speaker's name is changed to {Name}");
    }

    private void setLocation(string Name)
    {
      /*Rewrite this code later to actually change the outcome in the game. */
      Debug.Log($"The speaker's name is changed to {Name}");
    }


}
