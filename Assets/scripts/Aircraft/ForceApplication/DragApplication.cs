using UnityEngine;

public class DragApplication : MonoBehaviour
{
    //reference  aerocalculator script to access computed drag force values
    public AeroCalculator aeroCalc;
    public Rigidbody rb;    
    public Vector3 velocity;
    public float velocityMag;
    public float currentDragForce;



    void Start()
    {
        //get the rigidbody component attached to the spitfire obj
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("rigidbody not found in dragapplication");
        }
    }

    //FixedUpdate used for the physical force application 
    void FixedUpdate()
    {
        // get current drag force from aerocalculator (calculated in fixedupdate there)
        currentDragForce = aeroCalc.dragForce;
        
        // get the current velocityvector of the aircraft
        velocity = rb.velocity;
        velocityMag = velocity.magnitude;

        //if velocity is negligibly small, drag isnt calculated to avoid division by 0
        if (velocity.sqrMagnitude < 0.00001f)
        {
            // calculate the drag force direction as opposite to velocity
            Vector3 dragDirection = -velocity.normalized;   //normalised vector = only its direction (unit vector in the direction)
            // apply the drag force at the aircraft's center of mass
            rb.AddForce(dragDirection * currentDragForce, ForceMode.Force); 
            //ForceMode.Force clarifies the force is in Newtons, rather than e..g an impulse or acceleration
        }
    }
}
