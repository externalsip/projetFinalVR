using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloat : MonoBehaviour
{

    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    private bool isObjectInWater = false;


    private void FixedUpdate()
    {
        if (isObjectInWater)
        {
            rb.AddForceAtPosition(Physics.gravity, transform.position, ForceMode.Acceleration);
            float waveHeight = handleWave.instance.GetWaveHeight(transform.position.x);
            Debug.Log("triggered");
            if(transform.position.y < waveHeight)
            {
                Debug.Log("float");
                float displacementMultiplier = Mathf.Clamp01((waveHeight-transform.position.y) / depthBeforeSubmerged) * displacementAmount;
                rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "water")
        {
            isObjectInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "water")
        {
            isObjectInWater = false;
        }
    }
}