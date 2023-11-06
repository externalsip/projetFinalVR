using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS A PROTOTYPE, THIS IS WHY IT IS NOT IN VR AND USES WEIRD KEYS IT IS ONLY MEANT FOR TESTING!!!!!!!!!

public class timingGamePrototype : MonoBehaviour
{
    private int playerScore = 5;
    private bool BadPress = false;
    private bool GoodPress = false;
    private bool GreatPress = false;
    private bool PerfectPress = false;
    private bool IsFishOnHook = false;
    private IEnumerator FishRountine;

    public TextAsset textJSON;
    [System.Serializable]
    public class Fish
    {
        public string name;
        public int worth;
        public string mesh;
    }

    [System.Serializable]
    public class FishList
    {
        public Fish[] fishes;
    }

    public FishList myFishList = new FishList();

    // Start is called before the first frame update
    void Start()
    {
        myFishList = JsonUtility.FromJson<FishList>(textJSON.text);
        //Defines the routine used for the fish RNG and starts it (it is a test which is why the routine is started on launch)
        FishRountine = FishRng();
        StartCoroutine(FishRountine);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //For testing reasons, stopping the rountine is mapped on a button, it will not be this way in the final product.
            StopCoroutine(FishRountine);
            Debug.Log("Pressed P");
        }
        if (IsFishOnHook)
        {
            //While the bool IsFishOnHook is true, this code loops in the update
            if (Input.GetKeyDown(KeyCode.S) && IsFishOnHook)
        {
                //Depending on when the S key is hit, you get a different score.
            if(BadPress == true)
            {
                Debug.Log("Bad");
                playerScore--;

            }
            if(GoodPress == true)
            {
                Debug.Log("Good");
                playerScore++;
            }
            if(GreatPress == true)
            {
                Debug.Log("Good");
                playerScore = playerScore + 2;
            }
            if(PerfectPress == true)
            {
                Debug.Log("Perfect");
                playerScore = playerScore + 3;
            }
            this.GetComponent<Animator>().Play("Base Layer.cubeAnimation", 0, 0);
        }
        if(playerScore >= 20)
        {
                //If the player gets more than 20 points, he wins, what this entails has not been programmed yet, as I will need to figure out some stuff first.
            Debug.Log("Big Win");
                this.GetComponent<Animator>().Play("Base Layer.Idle", 0, 0);
                IsFishOnHook = false;
        }
        if(playerScore < 0)
        {
                //If the player gets to 0 points, he loses, for now it has no repercussions, but if we end up doing the roguelite he will lose a bait.
            Debug.Log("L");
                this.GetComponent<Animator>().Play("Base Layer.Idle", 0, 0);
                IsFishOnHook = false;
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
        BadPress = true;
        GoodPress = false;
        GreatPress = false;
        PerfectPress = false;
    }
    public void switchGood()
    {
        BadPress = false;
        GoodPress = true;
        GreatPress = false;
        PerfectPress = false;
    }
    public void switchGreat()
    {
        BadPress = false;
        GoodPress = false;
        GreatPress = true;
        PerfectPress = false;
    }
    public void switchPerfect()
    {
        BadPress = false;
        GoodPress = false;
        GreatPress = false;
        PerfectPress = true;
    }

    //The following functions are what will actually trigger the loop in the real game, as the hook will trigger the coroutine when it goes in the water.
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "water":
                StartCoroutine(FishRountine);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "water":
                StopCoroutine(StartCoroutine(FishRountine));
                break;
        }
    }

    //The fishRNG coroutine rolls a number every few seconds, if the number is correct, the fishing minigame start, this will be enhanced sometimes in the future to also run the rng of what fish was caught depending on some factors such as where the player is or what time it is, the fish itself will also determine what the difficulty of the mini-game will be.
    private IEnumerator FishRng()
    {
        Debug.Log("Rolling");
        int FishNumber = Random.Range(0, 4);
        yield return new WaitForSeconds(3f);
        Debug.Log(FishNumber);
        if(FishNumber == 3)
        {
            Debug.Log("Fish!!!");
            IsFishOnHook = true;
            this.GetComponent<Animator>().Play("Base Layer.cubeAnimation", 0, 0);
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(FishRountine);
            yield return null;
        }
    }
}