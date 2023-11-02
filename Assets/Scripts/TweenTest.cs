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


public class TweenTest : MonoBehaviour
{
    
    public string actorName;

    public List<EmotionPair> emotions;

    private void TR_Basic(float x, float y)
    {
        transform.TweenPosition(new Vector3(x*Screen.width,y*Screen.height,0f),0.2f);
    }

    public void TR_LeftCenter()
    {
        TR_Basic(0.4f,0.5f);
    }

    public void TR_LeftCenterDown()
    {
        TR_Basic(0.2f,0.4f);
    }


    public void TR_RightCenter()
    {
        TR_Basic(0.6f,0.5f);
    }

    
    public void TR_RightCenterDown()
    {
        TR_Basic(0.8f,0.4f);
    }

    public void SetImage(string command)
    {
        var img = emotions.Find(emotion => emotion.name == command);
        this.gameObject.GetComponent<Image>().sprite = img.value;
    }


}
