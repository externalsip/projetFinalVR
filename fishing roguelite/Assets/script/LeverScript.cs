using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject boat;
    private Rigidbody rb;
    private bool isLeverOn = false;
    public float maxSpeed = 1f;
    private IEnumerator stop;
    // Start is called before the first frame update
    void Start()
    {
        rb = boat.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeverOn)
        {
            rb.AddForce(-10, 0, 0, ForceMode.Acceleration);
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
                if(stop != null)
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
