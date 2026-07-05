using UnityEngine;

public class ThrustApplication : MonoBehaviour
{
    // this is the maximum thrust force the aircraft can produce
    public float maxThrust = 6000;
    // this is the current thrust force being applied
    public float currentThrust = 0f;
    // this is the increment per input press (as a proportion of maxThrust)
    public float thrustIncrement = 0.1f;
    public float degreeOffset = -4f;
    public Rigidbody rb;
    void Start()
    {   
        // get the rigidbody component attached to the aircraft
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            Debug.LogError("rigidbody not found");
        }
    }
    void FixedUpdate()
    {
        //rotating 4 degrees clockwise around y
        Quaternion offset = Quaternion.Euler(0f, degreeOffset, 0f);
        Vector3 thrustDirection = offset * -transform.forward;

        // get thrust force accounting for the offset by using newly calculated thrustDirection vector
        Vector3 thrustForce = thrustDirection * currentThrust;
        // apply the calculated force
        rb.AddForce(thrustForce);
    }

    // this method increases the thrust based on user input
    public void IncreaseThrust()
    {   
        //Debug.Log("Increase thrust method being run");
        currentThrust += maxThrust * thrustIncrement;
        //ensures current thrust being applied to the plane remains in between 0 and maxThrust inclusively
        currentThrust = Mathf.Clamp(currentThrust, 0f, maxThrust);
    }

    // this method decreases the thrust based on user input
    public void DecreaseThrust()
    {
        currentThrust -= maxThrust * thrustIncrement;
        currentThrust = Mathf.Clamp(currentThrust, 0f, maxThrust);
    }     
}


