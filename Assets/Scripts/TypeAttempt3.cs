using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Constants;


public class TypeAttempt3 : MonoBehaviour
{
    
    Dictionary<char,string> colorIndex = new Dictionary<char,string>()
    {
        {'R',"red"},
        {'G',"green"},
        {'B',"blue"},
        {'Y',"yellow"}
    };

    public float typeSpeed = 0.05f;
    public float timeDivisor = 30;
    public DialogueInterpreture dialogueMaster = null;

    private string userCommand = NULL;
    private char swivel = UNDERSCORE;
    string unallowed = ANGLE_BRACKET+ NEWLINE + "\b\r";

    
    [SerializeField]
    int pos;

    [MultilineAttribute]
    public string curPut = NULL;
    private string rawShown = NULL;

    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeLoop());
    }

    public void Restart()
    {
        rawShown = "";
    }

    // What does tok mean?
    int SeekNext(string a,int start,string tok)
    {
        int endchar = a.Substring(start).IndexOf(tok);
        return endchar + 1;
    }


    IEnumerator TypeLoop()
    {
        pos = 0;
        int offset = 0;
        while (true)
        {
            int combo = pos + offset;
            if (combo >= curPut.Length) { break; }

            char c = curPut[combo];


            //Toby Fox style pause shorthand.
            if (c == CARET)
            {
                float new_speed = float.Parse(curPut[combo + 1].ToString());
                yield return new WaitForSeconds(new_speed/timeDivisor);
                offset += 2;
                continue;
            }

            //Toby Fox style newline shorthand.
            if (c == AMPERSAND)
            {
                offset += 1;
                rawShown += NEWLINE;
                continue;
            }

            
            // Toby Fox style coloration shorthand.
            if (c == BACKSLASH)
            {
                char color = curPut[combo + 1];
                offset += 2;
                rawShown += "<color=\"";
                string cs = WHITE; //What is cs?
                cs = colorIndex[color];

                rawShown += cs;
                rawShown += "\">";
                continue;
            }

            //Allows user to add TMPRO tags to the input text.
            if(c == ANGLE_OPEN)
            {
                int endchar = SeekNext(curPut, combo, ">");
                string tag = curPut.Substring(combo, endchar);
                offset += endchar;
                rawShown += tag;
                continue;   
            }

            //Special tags related to time, returns, and waiting for input from the user.
            if (c == BRACE_OPEN)
            {
                int endchar = SeekNext(curPut, combo, BRACE_CLOSE.ToString());
                float tag = float.Parse(curPut.Substring(combo + 2, endchar - 3));
                switch(curPut[combo+1])
                {
                    case 's':
                        typeSpeed = tag;
                        break;
                    case 'w':
                        timeDivisor = tag;
                        break;
                    case 'r':
                        yield return new WaitForSeconds(tag);
                        rawShown = NULL;
                        break;
                    case 'i':
                        yield return new WaitUntil(()=> Input.GetKeyDown(SPACE_KEY));
                        rawShown = NULL;
                        break;
                    case 't':
                        StartCoroutine(SwivelCarat());
                        StartCoroutine(UserInput());
                        yield break;
                        break;

                }
                offset += endchar;
                
                continue;

            }

            if(c==SLASH)
            {
                if(curPut[combo+1]==PERCENT)
                {   
                    if(dialogueMaster != null)
                    {
                    curPut = NULL;
                    pos = 0;
                    offset = 0;
                    dialogueMaster.AskNext();
                    continue;
                    }
                }
            }


            pos++;
            rawShown += c;
            textMesh.text = rawShown;
            yield return new WaitForSeconds(typeSpeed);

        }

    }

    IEnumerator UserInput()
    {
        while(true)
        {
        yield return new WaitUntil(()=> Input.anyKey);
        string c = Input.inputString;
        if(c=="\b" && userCommand.Length > 0)
        {
            userCommand = userCommand.Substring(0,userCommand.Length-1);
            Debug.Log("beef");
            c =NULL;
        }

        if(c=="\r")
        {
            Debug.Log("submitting");
        }

        if(c!=NULL && !unallowed.Contains(c))
        {
        userCommand += Input.inputString;
        }
        textMesh.text = rawShown + userCommand + swivel;
        }
    }


    IEnumerator SwivelCarat()
    {
        char[] charRay = { UNDERSCORE, BACKSLASH, 'I', SLASH };
        int charAt = 0;
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
            swivel = charRay[charAt];
            textMesh.text = rawShown + userCommand + swivel;
            charAt++;
            if(charAt >= charRay.Length){charAt = 0;}
        }
    }

}
