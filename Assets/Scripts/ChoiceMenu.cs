using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static Constants;
using Unity.VisualScripting;

public class ChoiceMenu : MonoBehaviour
{

    public GameObject firstButton;
    public DialogueInterpreture gameManager;
    public ButtonManager buttonManager;


    public void AnalyseChoices(string value)
    {
        List<string> choices = new(value.Split(COMMA));
        List<string> choiceDestination = new();
        List<string> buttonName = new();
        foreach(string choice in choices)
        {
            string[] args = choice.Split(COLON);
            choiceDestination.Add(args[0]);
            buttonName.Add(args[1]);
        }
        GenerateButtons(buttonName, choiceDestination);
    }

    private void GenerateButtons(List<string> buttonName, List<string> choiceDestination)
    {
        int numButtons = buttonName.Count;
        GameObject buttonPrefab = firstButton;
        GameObject buttonParent = GameObject.Find("ChoiceMenu");
        Transform buttonParentTransform = buttonParent.transform;

        float spacing = 10f; // Adjust this value to control the spacing between buttons

        float buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width; // Assuming RectTransform is used for the button
        float totalWidth = numButtons * buttonWidth + (numButtons - 1) * spacing;

        float startX = buttonParentTransform.position.x - totalWidth / 2f;

        for (int i = 0; i < numButtons; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(buttonParentTransform);
            // Calculate the X position based on button width, spacing, and total width
            float xOffset = startX + i * (buttonWidth + spacing);
            Vector3 newPosition = new(xOffset, buttonParentTransform.position.y, buttonParentTransform.position.z);
            newButton.transform.position = newPosition;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName[i];
            newButton.name = buttonName[i];
            // Add a string component to the button

            buttonManager.dialogueFile = choiceDestination[i];
            //newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(choiceDestination[i]));
        }
    }
}
