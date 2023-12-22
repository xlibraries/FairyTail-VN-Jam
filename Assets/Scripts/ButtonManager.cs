using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
public class ButtonManager : MonoBehaviour
{

    public DialogueInterpreture gameManager;

    public string dialogueFile = "";

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<DialogueInterpreture>();
    }

    public void OnButtonClick()
    {
            gameManager.LoadNewCRD(dialogueFile);
            DestroyChildren();
    }

    // Destroy all the childerns of the parent object
    public void DestroyChildren()
    {
        foreach (Transform child in transform.parent)
        {
            Destroy(child.gameObject);
        }
    }
}