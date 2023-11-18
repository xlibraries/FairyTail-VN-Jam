using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zigurous.Tweening;

[System.Serializable]
public struct EmotionPair
{
    public string name;
    public Sprite value;
}


/*
Default actor class. Every actor will use a "dictionary" of name-sprite pairs to determine what emotion to use.
Will include transforms for flipping horizontally if needed, as well as moving and bobbing around on screen.

*/

public class TweenTest : MonoBehaviour
{
    
    public string actorName;

    public List<EmotionPair> emotions;

    private void TRBasic(float x, float y)
    {
        transform.TweenPosition(new Vector3(x*Screen.width,y*Screen.height,0f),0.2f);
    }

    public void TRLeftCenter()
    {
        TRBasic(0.4f,0.5f);
    }

    public void TRLeftCenterDown()
    {
        TRBasic(0.2f,0.4f);
    }


    public void TRRightCenter()
    {
        TRBasic(0.6f,0.5f);
    }

    
    public void TRRightCenterDown()
    {
        TRBasic(0.8f,0.4f);
    }

    public void SetImage(string command)
    {
        var img = emotions.Find(emotion => emotion.name == command);
        this.gameObject.GetComponent<Image>().sprite = img.value;
    }


}
