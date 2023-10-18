using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class type_attempt_3 : MonoBehaviour
{
    
    public float typeSpeed = 0.05f;
    public float timeDivisor = 30;
    public GameObject next;
    
    [SerializeField]
    int pos;

    [MultilineAttribute]
    public string curPut = "";
    string rawShown = "";

    TextMeshProUGUI textmesh;
    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(typeLoop());
    }

    int seeknext(string a,int start,string tok)
    {
        int endchar = a.Substring(start).IndexOf(tok);
        return endchar + 1;
    }


    IEnumerator typeLoop()
    {
        pos = 0;
        int offset = 0;
        while (true)
        {
            int combo = pos + offset;
            if (combo >= curPut.Length) { break; }

            char c = curPut[combo];


            //Toby Fox style pause shorthand.
            if (c == '^')
            {
                float new_speed = float.Parse(curPut[combo + 1].ToString());
                yield return new WaitForSeconds(new_speed/timeDivisor);
                offset += 2;
                continue;
            }

            //Toby Fox style newline shorthand.
            if (c == '&')
            {
                offset += 1;
                rawShown += "\n";
                continue;
            }

            
            // Toby Fox style coloration shorthand.
            if (c == '\\')
            {
                char color = curPut[combo + 1];
                offset += 2;
                rawShown += "<color=\"";
                string cs = "white";
                switch (color)
                {
                    case 'R':
                        cs = "red";
                        break;
                    case 'G':
                        cs = "green";
                        break;
                    case 'Y':
                        cs = "yellow";
                        break;

                }

                rawShown += cs;
                rawShown += "\">";
                continue;
            }

            //Allows user to add TMPRO tags to the input text.
            if(c == '<')
            {
                int endchar = seeknext(curPut, combo, ">");
                string tag = curPut.Substring(combo, endchar);
                offset += endchar;
                rawShown += tag;
                continue;   
            }

            //Special tags related to time, returns, and waiting for input from the user.
            if (c == '{')
            {
                int endchar = seeknext(curPut, combo, "}");
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
                        rawShown = "";
                        break;
                    case 'i':
                        yield return new WaitUntil(()=> Input.GetKeyDown("space"));
                        rawShown = "";
                        break;
                    case 't':
                        StartCoroutine(swivelCarat());
                        StartCoroutine(userInput());
                        yield break;
                        break;

                }
                offset += endchar;
                
                continue;

            }

            if(c=='/')
            {
                if(curPut[combo+1]=='%')
                {
                    yield return new WaitForSeconds(1.0f);
                    Destroy(gameObject);
                    if(next != null)
                    {
                        next.SetActive(true);
                    }
                    break;
                }
            }


            pos++;
            rawShown += c;
            textmesh.text = rawShown;
            yield return new WaitForSeconds(typeSpeed);

        }

    }


    string userCommand = "";
    char swivel = '_';
    string unallowed = "<>\n\b\r";
    IEnumerator userInput()
    {
        while(true)
        {
        yield return new WaitUntil(()=> Input.anyKey);
        string c = Input.inputString;
        if(c=="\b" && userCommand.Length > 0)
        {
            userCommand = userCommand.Substring(0,userCommand.Length-1);
            Debug.Log("beef");
            c ="";
        }

        if(c=="\r")
        {
            Debug.Log("submitting");
        }

        if(c!="" && !unallowed.Contains(c))
        {
        userCommand += Input.inputString;
        }
        textmesh.text = rawShown + userCommand + swivel;
        }
    }


    IEnumerator swivelCarat()
    {
        char[] char_ray = {'_','\\','I','/'};
        int char_at = 0;
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
            swivel = char_ray[char_at];
            textmesh.text = rawShown + userCommand + swivel;
            char_at++;
            if(char_at >= char_ray.Length){char_at = 0;}
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
