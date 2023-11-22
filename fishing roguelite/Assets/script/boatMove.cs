using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatMove : MonoBehaviour
{
    public GameObject leftHint;
    public GameObject rightHint;

    private Vector3 rightPos;

    private Vector3 leftPos;

    private Vector3 oldRightPos;

    private Vector3 oldLeftPos;

    private Vector3 forward = new Vector3 (1f, 0f, 0f);

    public bool rightSelected = false;
    public bool leftSelected = false;
    public bool bothSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        rightSelected = false;
        leftSelected = false;
        bothSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        rightPos = rightHint.transform.rotation.eulerAngles;
        rightPos.y = 0f;
        leftPos = leftHint.transform.rotation.eulerAngles;
        leftPos.y = 0f;
        if (this.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f)
        {

            Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
            if (bothSelected)
            {
                Debug.Log("both");
                this.GetComponent<Rigidbody>().AddForce(((rightPos.x + leftPos.x) / 20) * Time.deltaTime, 0f, 0f);
            }

            else if (leftSelected)
            {
                if (leftPos.x / oldLeftPos.x >= 2)
                {
                    Debug.Log("left");
                    this.GetComponent<Rigidbody>().AddForce(0f, 0f, (leftPos.x / 15) * Time.deltaTime);
                }
            }

            else if (rightSelected)
            {
                if (rightPos.x / oldRightPos.x >= 2)
                {
                    Debug.Log("right");
                    this.GetComponent<Rigidbody>().AddForce(0f, 0f, (rightPos.x / 15) * Time.deltaTime);
                }

            }
            else
            {
                if(this.GetComponent<Rigidbody>().velocity.magnitude > 0f){
                    this.GetComponent<Rigidbody>().velocity *= 0.2f * Time.deltaTime;
                }
            }
            oldRightPos = rightPos;
            oldLeftPos = leftPos;
        }
    }

    public void selectRight()
    {
        rightSelected = true;
        if (leftSelected)
        {
            bothSelected = true;
        }
    }

    public void selectLeft()
    {
        leftSelected = true;
        if(rightSelected)
        {
            bothSelected = true;
        }
    }

    public void exitselectRight() {
        rightSelected = false;
        bothSelected = false;
    }

    public void exitSelectedLeft()
    {
        leftSelected = false;
        bothSelected = false;
    }

}
