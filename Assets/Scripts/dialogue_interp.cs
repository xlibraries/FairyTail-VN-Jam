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
      if(d_pos <= dialogueChunks.Length)
      {
      pairedDialogueBox.curPut = dialogueChunks[d_pos] + "/%";
      d_pos += 1;
      }
    }

}
