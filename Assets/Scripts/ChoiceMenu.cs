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
    public HashSet<string> smartExhaustedOptions = new HashSet<string>();



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

    private void ButtonsExhaustive(string[] data)
    {
    List<string> args = new List<string>(data);
    string finalDestination = args[0];
    args.RemoveAt(0);
    bool everyButtonClicked = true;

    Debug.Log($"This menu will proceed to {finalDestination}");
    Debug.Log(string.Join(", ",smartExhaustedOptions));

    

    for (int i = 0; i < args.Count-1; i+=2)
        {
       
            string buttonText = args[i];
            string buttonDestination = args[i+1];
            if(smartExhaustedOptions.Contains(buttonDestination)) {return;}
            else { everyButtonClicked = false;}

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
            newButton.GetComponent<Button>().onClick.AddListener(() => LinkWrapperExhuastive(buttonDestination));

        }

        if(everyButtonClicked)
        {
            Debug.Log("END STATE REACHED");
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

    void LinkWrapperExhuastive(string destination)
    {
        //exhaustedOptions.Add(destination);
        smartExhaustedOptions.Add(destination);
        LinkWrapper(destination);
    }


    void LinkWrapper(string destination)
    {
        gameManager.LoadNewCRD(destination);
        gameObject.SetActive(false);
    }

}

