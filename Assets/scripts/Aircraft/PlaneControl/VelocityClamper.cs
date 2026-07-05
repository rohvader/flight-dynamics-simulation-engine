using UnityEngine;

public class VelocityLimiter : MonoBehaviour
{
    public SpitfireMKVIII planeData;
    public float maxSpeed;
    
    private Rigidbody rb;
    
    void Start()
    {
        // get the rigidbody component attached to the aircraft
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.LogError("rigidbody not found");
        }
    }
    void FixedUpdate()
    {
        maxSpeed = planeData.maxSpeed;
        // check if the current velocity exceeds the maximum allowed velocity
        if(rb.velocity.magnitude > maxSpeed)
        {
            // clamp the velocity by setting it to the maximum in the same direction
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
