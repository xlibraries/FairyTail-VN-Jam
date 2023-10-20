using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


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
          dialogueChunks = dialogueFile.text.Split("/%");
          askNext();
    }

    public void askNext()
    {
      if(d_pos < dialogueChunks.Length) //While there are chunks left , add a chunk one at a time to the dialogue box.
      {
      string next = dialogueChunks[d_pos] + "/%";
      Debug.Log(next);
      if(next.StartsWith("SCENE")) // If this is a SCENE chunk, parse it as such.
      {
        Debug.Log("Parsing scene");
        parseScene(next);
        d_pos += 1;
        askNext();
        return;
      }
      next = next.Replace("\n","{i0}\n");
      pairedDialogueBox.curPut = next;
      d_pos += 1;
      }
    }


    private void parseScene(string Scene)
    {
      Scene = Scene.Replace("/%","");
      string[] tokens = Scene.Split(" ");
      for(int i = 0; i < tokens.Length; i++)
      {
        //Debug.Log(tokens[i]);
        switch(tokens[i])
        {
          case "speaking":
            changeSpeakerName(tokens[i+1]);
            break;
        }
      }
    }

    private void changeSpeakerName(string Name)
    {
      /*Rewrite this code later to actually change the outcome in the UI. */
      Debug.Log($"The speaker's name is changed to {Name}");
    }

}
