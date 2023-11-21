using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//THIS IS A PROTOTYPE, THIS IS WHY IT IS NOT IN VR AND USES WEIRD KEYS IT IS ONLY MEANT FOR TESTING!!!!!!!!!

public class timingGamePrototype : MonoBehaviour
{
    private int playerScore = 5;
    private bool BadPress = false;
    private bool GoodPress = false;
    private bool GreatPress = false;
    private bool PerfectPress = false;
    public bool IsFishOnHook = false;
    public IEnumerator FishRountine;

    public fishArray fishArray;

    public fishingRodTests fishingRodTests;

    private bool firstActivation = true;

    // Start is called before the first frame update
    void Start()
    {
        //Defines the routine used for the fish RNG and starts it (it is a test which is why the routine is started on launch)


    }

    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        switch (fishingRodTests.controllerNum)
        {
            case 0:
                var desiredCharacteristicsLeft = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
                UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsLeft, inputDevices);
                break;
            case 1:
                var desiredCharacteristicsRight = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
                UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsRight, inputDevices);
                break;

        }
        bool primBtnValue;


        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(FishRountine);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //For testing reasons, stopping the rountine is mapped on a button, it will not be this way in the final product.
            Debug.Log("Pressed P");
            StopCoroutine(FishRountine);
        }
        if (IsFishOnHook)
        {
            foreach (var device in inputDevices)
            {

                //While the bool IsFishOnHook is true, this code loops in the update
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primBtnValue) && primBtnValue && firstActivation)
                {
                    Debug.Log("press");
                    //Depending on when the S key is hit, you get a different score.
                    if (BadPress == true)
                    {
                        Debug.Log("Bad");
                        playerScore--;
                    }
                    if (GoodPress == true)
                    {
                        Debug.Log("Good");
                        playerScore++;
                    }
                    if (GreatPress == true)
                    {
                        Debug.Log("Good");
                        playerScore = playerScore + 2;
                    }
                    if (PerfectPress == true)
                    {
                        Debug.Log("Perfect");
                        playerScore = playerScore + 3;
                    }
                    firstActivation = false;
                    Debug.Log(firstActivation);
                    this.GetComponent<Animator>().Play("Base Layer.cubeAnimation", 0, 0);
                }
                else if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primBtnValue) && primBtnValue == false && firstActivation == false)
                {
                    firstActivation = true;
                }
            }





        if(playerScore >= 20)
        {
                //If the player gets more than 20 points, he wins, what this entails has not been programmed yet, as I will need to figure out some stuff first.
            Debug.Log("Big Win");
                this.GetComponent<Animator>().Play("Base Layer.Idle", 0, 0);
                int fishNumber = Random.Range(50, 100);
                 if(fishNumber >= 1 && fishNumber < 50)
                 {
                     fishArray.PickCommonFish();
                     Debug.Log("common");
                 }
                 if(fishNumber >= 50 && fishNumber < 90)
                 {
                     fishArray.PickUncommonFish();
                     Debug.Log("uncommon");
                 }
                 if(fishNumber >= 90 && fishNumber <= 100)
                 {
                     fishArray.PickRareFish();
                     Debug.Log("rare");
                 }
                IsFishOnHook = false;
                fishingRodTests.isFishing = false;

        }




        if(playerScore < 0)
        {
                //If the player gets to 0 points, he loses, for now it has no repercussions, but if we end up doing the roguelite he will lose a bait
                this.GetComponent<Animator>().Play("Base Layer.Idle", 0, 0);
                IsFishOnHook = false;
                fishingRodTests.isFishing = false;
        }
    }
        }
    public void miss()
    {
        //If the player does not hit the button before the end of the animation, it is considered a miss.
            Debug.Log("Miss");
            playerScore = playerScore - 3;
            this.GetComponent<Animator>().Play("Base Layer.cubeAnimation", 0, 0);
    }
    

    //The following 4 functions are linked to animation events, to determine what is the current result of the player hitting the key.
    public void switchBad()
    {
        Debug.Log("toGood");
        BadPress = true;
        GoodPress = false;
        GreatPress = false;
        PerfectPress = false;
    }
    public void switchGood()
    {
        Debug.Log("toGreat");
        BadPress = false;
        GoodPress = true;
        GreatPress = false;
        PerfectPress = false;
    }
    public void switchGreat()
    {
        Debug.Log("toPerfect");
        BadPress = false;
        GoodPress = false;
        GreatPress = true;
        PerfectPress = false;
    }
    public void switchPerfect()
    {
        Debug.Log("toBad");
        BadPress = false;
        GoodPress = false;
        GreatPress = false;
        PerfectPress = true;
    }

    //The following functions are what will actually trigger the loop in the real game, as the hook will trigger the coroutine when it goes in the water.

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "water":
                StopCoroutine(FishRountine);
                break;
        }
    }

    public void hookInWater()
    {
        FishRountine = FishRng().GetEnumerator();
        StartCoroutine(FishRountine);
    }

    public void hookOutwater()
    {
        Debug.Log("stopped");
        StopCoroutine(FishRountine);
    }

    //The fishRNG coroutine rolls a number every few seconds, if the number is correct, the fishing minigame start, this will be enhanced sometimes in the future to also run the rng of what fish was caught depending on some factors such as where the player is or what time it is, the fish itself will also determine what the difficulty of the mini-game will be.
    private IEnumerable FishRng()
    {
        Debug.Log("Rolling");
        int FishNumber = Random.Range(0, 4);
        yield return new WaitForSeconds(3f);
        Debug.Log(FishNumber);
        if(FishNumber == 3)
        {
            fishingRodTests.isFishing = true;
            Debug.Log("Fish!!!");
            playerScore = 5;
            IsFishOnHook = true;
            this.GetComponent<Animator>().Play("Base Layer.cubeAnimation", 0, 0);
            yield return null;
        }
        else
        {
            Debug.Log("reroll");
            yield return new WaitForSeconds(2f);
            StartCoroutine(FishRng().GetEnumerator());
            yield return null;
        }
    }

    public void startTest()
    {
        StartCoroutine(FishRountine);
    }
}