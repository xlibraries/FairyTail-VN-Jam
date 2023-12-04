using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{

    public GameObject firstButton;
    public DialogueInterpreture gameManager;
    public List<string> exhaustedOptions;


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


    private void ButtonsNonExhaustive(string[] args)
    {
    for (int i = 0; i < args.Length-1; i+=2)
        {
            GameObject newButton;
            string buttonText = args[i];
            string buttonDestination = args[i+1];

            if (i == 0)
            {
                newButton = firstButton;
            }
            else
            {
                newButton = (GameObject)Instantiate(firstButton);
            }

            newButton.transform.SetParent(firstButton.transform.parent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            newButton.GetComponent<Button>().onClick.AddListener(() => LinkWrapper(buttonDestination));

        }
    }

    private void ButtonsExhaustive(string[] args)
    {
    string finalDestination = args[0];
    args.RemoveAt(0);

    if(exhaustedOptions.Length == (args.Length-1)/2)
    {
      LinkWrapper(finalDestination);   
    }

    for (int i = 0; i < args.Length-1; i+=2)
        {
       
            string buttonText = args[i];
            string buttonDestination = args[i+1];
            if(exhaustedOptions.Contains(buttonDestination)) {return;}
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
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            newButton.GetComponent<Button>().onClick.AddListener(() => 
            {
                exhaustedOptions.Add(buttonDestination);
                LinkWrapper(buttonDestination)
            });

        }
    }



    public void TakeButtons(string data, bool exhaustive)
    {
        ClearButtons();
        string[] args = data.Split(",");
        if(exhaustive)
        {
            ButtonsExhaustive(args);
        }
        else
        {
            ButtonsNonExhaustive(args);
        }
    }


    void LinkWrapper(string destination)
    {
        gameManager.LoadNewCRD(destination);
        gameObject.SetActive(false);
    }

}

