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

    public float maxSpeed = 1f;

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
        Vector3 leftVelocity = leftHint.GetComponent<Rigidbody>().velocity;
        Vector3 rightVelocity = rightHint.GetComponent<Rigidbody>().velocity;
        if (this.GetComponent<Rigidbody>().velocity.magnitude <= 0.5f)
        {

            Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
            if (bothSelected)
            {
                Debug.Log("both");
                this.GetComponent<Rigidbody>().AddForce(((leftVelocity + rightVelocity) / 20) * Time.deltaTime);
            }

            else if (leftSelected)
            {
                if (leftPos.x / oldLeftPos.x >= 2)
                {
                    Debug.Log("left");
                    this.GetComponent<Rigidbody>().AddForce(0f, 0f, (leftVelocity.x) * Time.deltaTime);
                }
            }

            else if (rightSelected)
            {
                if (rightPos.x / oldRightPos.x >= 2)
                {
                    Debug.Log("right");
                    this.GetComponent<Rigidbody>().AddForce(0f, 0f, (rightVelocity.x / 15) * Time.deltaTime);
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
        this.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody>().velocity, maxSpeed);
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
