using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{

    Dictionary<string,TweenTest> actorDict;

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
