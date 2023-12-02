using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timingHint : MonoBehaviour
{
    public timingGamePrototype timing;
    public GameObject hook;

    private void Update()
    {
        this.transform.position = new Vector3(hook.transform.position.x, this.transform.position.y, hook.transform.position.z);
    }
    public void switchBad()
    {
        Debug.Log("toGood");
        timing.BadPress = true;
        timing.GoodPress = false;
        timing.GreatPress = false;
        timing.PerfectPress = false;
    }
    public void switchGood()
    {
        Debug.Log("toGreat");
        timing.BadPress = false;
        timing.GoodPress = true;
        timing.GreatPress = false;
        timing.PerfectPress = false;
    }
    public void switchGreat()
    {
        Debug.Log("toPerfect");
        timing.BadPress = false;
        timing.GoodPress = false;
         timing.GreatPress = true;
         timing.PerfectPress = false;
    }
    public void switchPerfect()
    {
        Debug.Log("toBad");
        timing.BadPress = false;
        timing.GoodPress = false;
        timing.GreatPress = false;
        timing.PerfectPress = true;
    }
}
