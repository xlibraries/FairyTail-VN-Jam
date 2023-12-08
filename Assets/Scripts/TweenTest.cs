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

    public void TRFlip()
    {
        transform.localScale = new Vector3(-1,1,1);
    }

    public void TRBob()
    {
        Vector3 oldPosition = transform.position;
        transform.TweenPosition(oldPosition + new Vector3(0f,30f,0f),0.2f)
        .OnComplete(()=>
        {
        transform.TweenPosition(oldPosition,0.2f);
        });
    }

    public void TRKillFade()
    {
      this.gameObject.GetComponent<Image>().TweenColor(Color.clear,0.2f)
      .OnComplete(() => {
        GameObject.Destroy(this.gameObject);
      });
    }

    public void TRFade()
    {
      this.gameObject.GetComponent<Image>().TweenColor(Color.clear,0.2f);
    }

    public void TRFadeIn()
    {
      this.gameObject.GetComponent<Image>().color = Color.clear;
      this.gameObject.GetComponent<Image>().TweenColor(Color.white,2.0f);
    }


    /*
    PushUp and PushDown are RELATIVE - wherever the Actor is, it will move them slightly up or down
    no matter where they are. Good for small twitches or twinges, or just talking.
    */
    public void TRPushUp()
    {
        Vector3 oldPosition = transform.position;
        transform.TweenPosition(oldPosition + new Vector3(0f,30f,0f),0.2f);
    }

    public void TRPushDown()
    {
        Vector3 oldPosition = transform.position;
        transform.TweenPosition(oldPosition - new Vector3(0f,30f,0f),0.2f);
    }

    public void SetImage(string command)
    {
        var img = emotions.Find(emotion => emotion.name == command);
        this.gameObject.GetComponent<Image>().sprite = img.value;
    }


}
