using UnityEngine;

public class AeroCalculator : MonoBehaviour
{
    public SpitfireMKVIII planeData;
    //plane's rigidbody component 
    private Rigidbody rb;
   
    //plane transform for directional calculations
    public Transform plane;

    //public attributes to track values in inspector
    public float altitude;
    public float airDensity;
    
    public float reynolds;
    public float cl;
    public float liftForce;
    public float reynoldsFactor_;
    public float xAlpha_;

    // additional variables for drag calculations 
    public float airspeed;  //moved airspeed lower to compare with drag values easily
    public float cd;
    public float dragForce;

    void Start()
    {
        // get the rigidbody component attached to the plane
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("rigidbody not found on start");
        }

        if (leftWing == null || rightWing == null)
        {
            Debug.LogError("wing not found");
        } 
    }
    void Update()
    {
        reynoldsFactor_ = Aerodynamics.reynoldsFactor;
        xAlpha_ = Aerodynamics.xAlpha;
    }
    // updating the physics related values which are used in both lift and drag calculations to avoid duplicate code
    void FixedUpdate()
    {
        altitude = plane.position.y; // update altitude using plane's y position
        airDensity = Aerodynamics.CalculateAirDensity(altitude); // calculate air density using altitude
        airspeed = rb.velocity.magnitude; // update airspeed from rigidbody velocity
        reynolds = Aerodynamics.CalculateReynoldsNumber(planeData, airspeed, airDensity); // update Reynolds number


        //could also be moved into DragApplication.cs or retrived (e.g. in DragApplication, I can say aoa = aeroCalc.aoa, cd = aeroCalc.cd etc.)
        float aoa = CalculateAngleOfAttack(); // calculate angle of attack

        // update drag calculations
        cd = Aerodynamics.GetDragCoefficient(planeData, aoa); // calculate drag coefficient using AoA
        dragForce = Aerodynamics.GetDragForce(planeData, airDensity, airspeed, cd); // calculate drag force using computed values
    }

    // calculates and returns the plane's angle of attack in degrees
    public float CalculateAngleOfAttack()
    {
        // get the plane's forward vector in world space
        Vector3 forward = plane.forward;
        // project forward vector onto the horizontal plane
        Vector3 forwardProjected = new Vector3(forward.x, 0f, forward.z).normalized;
        // calculate signed angle around plane's local right axis (positive for nose-up)
        float aoa = Vector3.SignedAngle(forwardProjected, forward, plane.right);
        // return angle with pitch offset applied
        return aoa + planeData.pitchOffset;
    }

    // calculates and returns the lift force based on current conditions
    public float CalculateLiftForce(float aoa)
    {
    // calculate lift coefficient using AoA and Reynolds number
    cl = Aerodynamics.GetLiftCoefficient(planeData, aoa, reynolds);

    // calculate lift force using computed values
    liftForce = Aerodynamics.GetLiftForce(planeData, airDensity, airspeed, cl);
    return liftForce;
    }

    // calculates and returns the lift force based on current conditions
    public float CalculateDragForce(float aoa)
    {
    // calculate drag coefficient using AoA (function from Aerodynamics.cs)
    cd = Aerodynamics.GetDragCoefficient(planeData, aoa);

    // calculate drag force using computed values
    dragForce = Aerodynamics.GetDragForce(planeData, airDensity, airspeed, cd);
    return dragForce;
    }
}
