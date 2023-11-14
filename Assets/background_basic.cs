using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zigurous.Tweening;

[System.Serializable]
public struct BackgroundPair
{
    public string name;
    public Sprite value;
}


/*
Test class for the background system. Very similar to the actor system, but backgrounds may need dedicated transitions and funky effects.
*/
public class background_basic : MonoBehaviour
{

    public List<BackgroundPair> backgrounds;


    public void setBGImage(string newBG)
    {
        Sprite nextSprite = backgrounds.Find(bg => bg.name == newBG).value;
        this.GetComponent<Image>().sprite = nextSprite;
    }    


    /*
    Initial function for switching backgrounds.

    c = Color to fade to
    dur = Duration of ENTIRE transition.
    nextOne = the next image. See the backgrounds dict in the scene for more information.
    */
    public void FadeColorSwitch(Color c,float dur, string nextOne)
    {
        Sprite nextSprite = backgrounds.Find(bg => bg.name == nextOne).value;
        Debug.Assert(nextSprite != null);

        var img = this.GetComponent<Image>();
        img.TweenColor(c,dur/2.0f).OnComplete(() => { //Fade out to the color chosen.
            img.sprite = nextSprite;
            img.TweenColor(Color.white,dur/2.0f);  //Fade in to the next image.
            });
    }
}
