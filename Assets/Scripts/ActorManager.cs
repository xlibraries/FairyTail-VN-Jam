using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

[System.Serializable]
public struct ActorPair
{
    public string name;
    public GameObject value;
}


public class ActorManager : MonoBehaviour
{

    Dictionary<string,TweenTest> actorDict;

    public List<ActorPair> actorPresets;

    public GameObject actorStage;

    public void GatherActors()
    {
        actorDict = new Dictionary<string, TweenTest>();

        Debug.Log("Finding all actors");
        var actorList = GameObject.FindGameObjectsWithTag("Actor");
        if(actorList.Length == 0 ) {return;}
        foreach (var actorGameObject in actorList)
        {
            var TT = actorGameObject.GetComponent<TweenTest>();
            actorDict[TT.actorName] = TT;
        }

        //SpawnActor("Lena,LeftCenter,angry");
    }

    //Replace with more sophisticated "fade in"
    public void SpawnActor(string command)
    {
        string[] args = command.Split(COMMA);
        string name = args[0];
        GameObject madeActor;
        if(actorDict.ContainsKey(name))
        {
            Debug.Log($"Actor {name} already detected. Proceeding to transforms. Side note: this should not be possible and is an error in tag wrangling.");
            //DoTransform($"{name},ScaleNormal");
            DoTransform($"{name},FixFlip");
            DoTransform($"{name},FadeIn");
        }
        else
        {
        GameObject storedActor = actorPresets.Find(actor => actor.name == name).value;
        Debug.Assert(storedActor != null);
        madeActor = GameObject.Instantiate(storedActor);
        madeActor.transform.parent = actorStage.transform;
        var TT = madeActor.GetComponent<TweenTest>();
        actorDict[TT.actorName] = TT;
        }

        DoTransform($"{name},{args[1]}");
        SwitchImage($"{name},{args[2]}");
    }

    //Replace with more sophisticated "fade out"
    public void KillActor(string command)
    {
        DoTransform($"{command},KillFade");
        actorDict.Remove(command);
    }

    public void SpamActor(string command)
    {
      //Debug.Log("Trying to spam");
      string[] args = command.Split(COMMA);
      Debug.Assert(args.Length > 1);
      var grabbedActor = actorDict[args[0]];
      grabbedActor.TalkBob(args[1],args[2]);
    }

    public void FadeActorTo(string command)
    {
        string[] args = command.Split(COMMA);
        Debug.Assert(args.Length > 1);
        var grabbedActor = actorDict[args[0]];
        grabbedActor.GetComponent<TweenTest>().FadeImage(args[1]); 
    }



    public void DoTransform(string command)
    {
        string[] args = command.Split(COMMA);
        Debug.Assert(args.Length > 1);
        var grabbedActor = actorDict[args[0]];
        if(grabbedActor.gameObject.activeSelf)
        {
        var transformMethod = grabbedActor.GetType().GetMethod("TR" + args[1]);
        transformMethod.Invoke(grabbedActor,null);
        }
    }

    public void SwitchImage(string command)
    {
        string[] args = command.Split(COMMA);
        Debug.Assert(args.Length > 1);
        TweenTest grabbedActor = actorDict[args[0]];
        grabbedActor.SetImage(args[1]);
        
    }
}
