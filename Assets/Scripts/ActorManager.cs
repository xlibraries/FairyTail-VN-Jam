using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{

    Dictionary<string,TweenTest> actorDict;

    void Start()
    {
        actorDict = new Dictionary<string, TweenTest>();

        Debug.Log("Finding all actors");
        var ActorList = GameObject.FindGameObjectsWithTag("Actor");
        foreach (var actorGameObject in ActorList)
        {
            var TT = actorGameObject.GetComponent<TweenTest>();
            actorDict[TT.actorName] = TT;
        }


    }

    public void DoTransform(string command)
    {
        //var bandaid = DummyActor.GetComponent<TweenTest>();
        var bandaid = actorDict["Lena"];
        var transformMethod = bandaid.GetType().GetMethod("TR_" + command);
        transformMethod.Invoke(bandaid,null);
    }
}
