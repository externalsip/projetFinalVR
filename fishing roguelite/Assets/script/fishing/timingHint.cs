using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timingHint : MonoBehaviour
{
    public timingGamePrototype timing;


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

    public void miss()
    {
        //If the player does not hit the button before the end of the animation, it is considered a miss.
        Debug.Log("Miss");
        timing.playerScore = timing.playerScore - 3;
        this.GetComponent<Animator>().Play("Base Layer.spriteCircle", 0, 0);
    }
}
