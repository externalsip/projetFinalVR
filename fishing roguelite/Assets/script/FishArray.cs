using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class fishArray : MonoBehaviour
{
    public GameObject hook;

    public GameObject[] commonFishArr;
    public GameObject[] uncommonFishArr;
    public GameObject[] rareFishArr;
    public GameObject[] allFishArr;

    private GameObject pickedFish;

    public fishingRodTests fishingRodTests;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickCommonFish()
    {
        int fishNumber = Random.Range(0, commonFishArr.Length - 1);
        pickedFish = commonFishArr[fishNumber];
        Instantiate(pickedFish);
        pickedFish.transform.position = hook.transform.position;
        pickedFish.AddComponent<FixedJoint>();
        pickedFish.GetComponent<FixedJoint>().connectedBody = hook.GetComponent<Rigidbody>();
        /*
        var hookBody = hook.GetComponent<Rigidbody>();
        hookBody.velocity = Vector3.zero;
        hookBody.angularVelocity = Vector3.zero;
        hook.transform.position = fishingRodTests.hookHint.transform.position;
        hook.AddComponent<HingeJoint>();
        hook.GetComponent<HingeJoint>().connectedBody = fishingRodTests.rod.GetComponent<Rigidbody>();

        fishingRodTests.hasJoint = true;
        fishingRodTests.isThrown = false;*/
    }
    public void PickUncommonFish()
    {
        int fishNumber = Random.Range(0, uncommonFishArr.Length - 1);
        pickedFish = uncommonFishArr[fishNumber];
        Instantiate(pickedFish);
        Debug.Log("instantiated uncommon");
        pickedFish.gameObject.transform.position = hook.transform.position;
        pickedFish.gameObject.AddComponent<FixedJoint>();
        pickedFish.gameObject.GetComponent<FixedJoint>().connectedBody = hook.GetComponent<Rigidbody>();
/*
        var hookBody = hook.GetComponent<Rigidbody>();
        hookBody.velocity = Vector3.zero;
        hookBody.angularVelocity = Vector3.zero;
        hook.transform.position = fishingRodTests.hookHint.transform.position;
        hook.AddComponent<HingeJoint>();
        hook.GetComponent<HingeJoint>().connectedBody = fishingRodTests.rod.GetComponent<Rigidbody>();

        fishingRodTests.hasJoint = true;
        fishingRodTests.isThrown = false;*/
    }
    public void PickRareFish()
    {
        int fishNumber = Random.Range(0, rareFishArr.Length - 1);
        pickedFish = commonFishArr[fishNumber];
        Instantiate(pickedFish);
        Debug.Log("instantiated rare");
        pickedFish.gameObject.transform.position = hook.transform.position;
        pickedFish.gameObject.AddComponent<FixedJoint>();
        pickedFish.gameObject.GetComponent<FixedJoint>().connectedBody = hook.GetComponent<Rigidbody>();

      /*  var hookBody = hook.GetComponent<Rigidbody>();
        hookBody.velocity = Vector3.zero;
        hookBody.angularVelocity = Vector3.zero;
        hook.transform.position = fishingRodTests.hookHint.transform.position;
        hook.AddComponent<HingeJoint>();
        hook.GetComponent<HingeJoint>().connectedBody = fishingRodTests.rod.GetComponent<Rigidbody>();

        fishingRodTests.hasJoint = true;
        fishingRodTests.isThrown = false;*/

    }   
}
