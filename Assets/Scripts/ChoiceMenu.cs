using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{

    public GameObject firstButton;
    public DialogueInterpreture gameManager;
    //public HashSet<string> smartExhaustedOptions = new HashSet<string>();
    bool isOpen = true;
    
    
    public HashSet<string> smartExhaustedOptions = new HashSet<string>
    {
        "Act2/Act2Workshop.txt",
        "Act2/Act2Library.txt",
        "Act2/Act2Study.txt",
    };
    

    void Awake()
    {
        HideUI();
    }

    private void ClearButtons()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        firstButton.SetActive(true);
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
    firstButton.SetActive(false);


    for (int i = 0; i < args.Count-1; i+=2)
        {
       
            string buttonText = args[i];
            string buttonDestination = args[i+1];
            if(smartExhaustedOptions.Contains(buttonDestination)) {continue;}
            else { everyButtonClicked = false;}
            GameObject newButton;
            newButton = (GameObject)Instantiate(firstButton);

            newButton.SetActive(true);
            newButton.transform.SetParent(firstButton.transform.parent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            newButton.GetComponent<Button>().onClick.AddListener(() => LinkWrapperExhuastive(buttonDestination));

        }

        if(everyButtonClicked)
        {
            LinkWrapper(finalDestination);
            smartExhaustedOptions.Clear();
            finalDestination = "";
        }
    }


    public void TakeButtons(string data, bool exhaustive)
    {
        ClearButtons();
        ShowUI();
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
        if(!isOpen) {return;}
        //exhaustedOptions.Add(destination);
        smartExhaustedOptions.Add(destination);
        LinkWrapper(destination);
    }


    void LinkWrapper(string destination)
    {
        if(!isOpen) {return;}
        gameManager.LoadNewCRD(destination);
        HideUI();
    }

    public void HideUI()
    {
        gameObject.transform.localScale = new Vector3(0,0,0);
        isOpen = false;
    }

    public void ShowUI()
    {
        gameObject.transform.localScale = Vector3.one;
        isOpen = true;
    }

}

