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

    private void TRTeleportBasic(float x, float y)
    {
        transform.position = new Vector3(x*Screen.width,y*Screen.height,0f);
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

    public void TRCenter()
    {
        TRBasic(0.5f,0.5f);
    }

    public void TRWorkshopShield()
    {
            TRBasic(0.2f,0.3f);
    }

    public void TRWorkshopBottle()
    {
           TRBasic(0.8f,0.3f);
    }

    public void TRWorkshopHammer()
    {
           TRBasic(0.75f,0.3f);
    }

    public void TRFlip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

     public void TRFixFlip()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),Mathf.Abs(transform.localScale.y),Mathf.Abs(transform.localScale.z));
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
        Tweening.KillAll();
      });
    }



    public void TRFade()
    {
      this.gameObject.GetComponent<Image>().TweenColor(Color.clear,0.2f);
    }

    public void TRFadeIn()
    {
      this.gameObject.GetComponent<Image>().color = Color.clear;
      this.gameObject.GetComponent<Image>().TweenColor(Color.white,0.2f);
    }

    public void TRScaleDown()
    {
        Vector3 halfsize = transform.localScale * 0.5f;
        transform.TweenScale(halfsize,0.2f);
    }

    public void TRFourth()
    {
        TRBasic(0.25f,0.2f);
    }

    public void TRHalf()
    {
        TRBasic(0.5f,0.2f);
    }
    public void TRThreeFourth()
    {
        TRBasic(0.75f,0.2f);
    }

    public void TRDragonRoomSize()
    {
        Vector3 destinationSize = transform.localScale * 0.6f;
        transform.TweenScale(destinationSize,0.0f);
    }

    public void TRDragonRoomHeight()
    {
        float oldX = transform.position.x;
        transform.position = new Vector3(oldX,0.2f*Screen.height,0f);
    }

    public void TRScaleNormal()
    {
        Vector3 fullsize = Vector3.Normalize(transform.localScale);
        transform.TweenScale(fullsize,0.2f);
    }


     public void TRCenterFadeIn()
    {
      TRTeleportBasic(0.5f,0.5f);
      this.gameObject.GetComponent<Image>().color = Color.clear;
      this.gameObject.GetComponent<Image>().TweenColor(Color.white,0.2f);
    }

    public void TalkBob(string talkingImageName, string quietImageName)
    {
        var talkimg = emotions.Find(emotion => emotion.name == talkingImageName);
        var quietimg = emotions.Find(emotion => emotion.name == quietImageName);
        this.gameObject.GetComponent<Image>().sprite = talkimg.value;

        Vector3 oldPosition = transform.position;
        transform.TweenPosition(oldPosition + new Vector3(0f,30f,0f),0.2f)
        .OnComplete(()=>
        {
        transform.TweenPosition(oldPosition,0.2f);
        this.gameObject.GetComponent<Image>().sprite = quietimg.value;
        });
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

    public void FadeImage(string command)
    {

       var next_image = emotions.Find(emotion => emotion.name == command);
       GameObject faderActor = (GameObject)Instantiate(gameObject);
       
       faderActor.transform.SetParent(gameObject.transform.parent);
       faderActor.transform.localScale = transform.localScale;
       faderActor.transform.position = transform.position;
       faderActor.GetComponent<Image>().sprite = next_image.value;
       faderActor.GetComponent<Image>().TweenColor(Color.clear,4.0f);

       GetComponent<Image>().color = Color.clear;
       GetComponent<Image>().TweenColor(Color.white,4.0f);


    }


}
