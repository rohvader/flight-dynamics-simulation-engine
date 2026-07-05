using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunctions : MonoBehaviour

{   
    private Rigidbody rb;
    public float multiplier = 500f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
   
    //pitching
    public void PitchUp()
    {
        rb.AddTorque(transform.right * multiplier, ForceMode.Impulse);
    }
    public void PitchDown()
    {
        rb.AddTorque(transform.right * -multiplier, ForceMode.Impulse);
    }

    //rolling
    public void RollRight()
    {

        rb.AddTorque(transform.forward * multiplier, ForceMode.Impulse);
    }
    public void RollLeft()
    {
        
        rb.AddTorque(transform.forward * -multiplier, ForceMode.Impulse);
    }

     // yaw left and right
    public void YawLeft()
    {
        Debug.Log("yawing left using torque");
        rb.AddTorque(transform.up * -multiplier, ForceMode.Impulse);
    }

    public void YawRight()
    {
        Debug.Log("yawing right using torque");
        rb.AddTorque(transform.up * multiplier, ForceMode.Impulse);
    }
}