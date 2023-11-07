using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject ActorStage;

    public void GatherActors()
    {
        actorDict = new Dictionary<string, TweenTest>();

        Debug.Log("Finding all actors");
        var ActorList = GameObject.FindGameObjectsWithTag("Actor");
        Debug.Assert(ActorList.Length > 0);
        foreach (var actorGameObject in ActorList)
        {
            var TT = actorGameObject.GetComponent<TweenTest>();
            actorDict[TT.actorName] = TT;
        }

        //SpawnActor("Lena,LeftCenter,angry");
    }

    //Replace with more sophisticated "fade in"
    public void SpawnActor(string command)
    {
        string[] args = command.Split(",");
        string name = args[0];
        GameObject storedActor = actorPresets.Find(actor => actor.name == name).value;
        GameObject madeActor = GameObject.Instantiate(storedActor);
        madeActor.transform.parent = ActorStage.transform;
        var TT = madeActor.GetComponent<TweenTest>();
        actorDict[TT.actorName] = TT;

        DoTransform(name + "," + args[1]);
        SwitchImage(name + "," + args[2]);
    }

    //Replace with more sophisticated "fade out"
    public void KillActor(string command)
    {
        GameObject actorToKill = actorPresets.Find(actor => actor.name == name).value;
        GameObject.Destroy(actorToKill);
    }


    public void DoTransform(string command)
    {
        string[] args = command.Split(",");
        Debug.Assert(args.Length > 1);
        var grabbedActor = actorDict[args[0]];
        var transformMethod = grabbedActor.GetType().GetMethod("TR_" + args[1]);
        transformMethod.Invoke(grabbedActor,null);
    }

    public void SwitchImage(string command)
    {
        string[] args = command.Split(",");
        Debug.Assert(args.Length > 1);
        TweenTest grabbedActor = actorDict[args[0]];
        grabbedActor.SetImage(args[1]);
        
    }
}
