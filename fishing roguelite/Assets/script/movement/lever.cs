using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lever : MonoBehaviour
{

    public GameObject boat;
    private Rigidbody rb;

    private bool isLeverOn = false;
    public float maxSpeed = 1f;
    public SteerScript steering;
    private IEnumerator stop;
    // Start is called before the first frame update
    void Start()
    {
        rb = boat.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        boat.transform.Rotate(0f, steering.currentRotate * Time.deltaTime, 0f);
        if (isLeverOn)
        {            

            rb.AddForce(boat.transform.forward * 10, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "LeverTrigger":
                Debug.Log("hit");
                isLeverOn = true;
                if (stop != null)
                {
                    StopCoroutine(stop);
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "LeverTrigger":
                Debug.Log("hit");
                stop = stopCountdown().GetEnumerator();
                StartCoroutine(stop);
                break;

        }
    }

    private IEnumerable stopCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        isLeverOn = false;
        yield return null;
    }
}
