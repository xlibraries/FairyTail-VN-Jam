using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{

    public GameObject firstButton;
    public DialogueInterpreture gameManager;


    private void ClearButtons()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i].gameObject != firstButton)
            {
                Destroy(buttons[i].gameObject);
            }
        }
    }


    public void TakeButtons(string data)
    {
        ClearButtons();
        string[] args = data.Split(",");

        for (int i = 0; i < args.Length-1; i+=2)
        {
            GameObject newButton;
            if (i == 0)
            {
                newButton = firstButton;
            }
            else
            {
                newButton = (GameObject)Instantiate(firstButton);
            }

            newButton.transform.SetParent(firstButton.transform.parent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = args[i];
            string buttonDestination = args[i + 1];
            newButton.GetComponent<Button>().onClick.AddListener(() => LinkWrapper(buttonDestination));

        }

    }

    void LinkWrapper(string destination)
    {
        gameManager.LoadNewCRD(destination);
        gameObject.SetActive(false);
    }

}

