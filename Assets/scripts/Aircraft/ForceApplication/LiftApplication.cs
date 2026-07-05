using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LiftApplication : MonoBehaviour
{
    // link to AeroCalc script, calculating lift on each wing
    public AeroCalculator aeroCalc;
    public WingControl wingControl;

    // these transforms are where the lift forces will be physically applied (assigned in the inspector)
    public Transform leftWingTransform;
    public Transform rightWingTransform;
    
     // instantiating left and right wing plane classes
    public WingControl leftWingObj;
    public WingControl rightWingObj;
    private Rigidbody rb;

    public float uncorrectedaoa;
    public float leftEffective_aoa;
    public float rightEffective_aoa;
    public float leftWingLift;
    public float rightWingLift;
    
    
    void Start()
    {
        // retrieving rigidbody component for applying forces
        rb = GetComponent<Rigidbody>();
        //so the correct hinge point for aileron rotation is used
        leftWingObj.SetAileronPivotSide("left");
        rightWingObj.SetAileronPivotSide("right");
    }
    
    void FixedUpdate()
    {
        //again ensuring each component is assigned properly + the script for lift calculations
        if (aeroCalc == null || rb == null || leftWingObj == null || rightWingTransform == null)
            return; 

        //// wing lift force calcs based on aileron deflections - lift now calculated individually for each wing to allow rolling due to effective AoA changes
        // original aoa calc for both wings based on plane overall pitch angle
        uncorrectedaoa = aeroCalc.CalculateAngleOfAttack();

        //aoa corrected considering the effect of aileron deflection for each wing individually
        leftEffective_aoa = leftWingObj.get_effective_aoa(aeroCalc.CalculateAngleOfAttack(), leftWingObj.get_leftAileronDeflection());
        rightEffective_aoa = rightWingObj.get_effective_aoa(aeroCalc.CalculateAngleOfAttack(), rightWingObj.get_rightAileronDeflection());

        //lift calculation based on adjusted aoa values
        leftWingLift = aeroCalc.CalculateLiftForce(leftEffective_aoa);
        rightWingLift = aeroCalc.CalculateLiftForce(rightEffective_aoa);
        
        // apply the lift at each wing's position if they have values
        if (float.IsNaN(leftWingLift))
        {
            leftWingLift = 0f;
        }
        else
        {   
            //force for left lift is added at the new transform for the left wing
            rb.AddForceAtPosition(leftWingTransform.up * leftWingLift, leftWingTransform.position);            
        }

        if (float.IsNaN(rightWingLift))
        {
            rightWingLift = 0f;
        }
        else{
            //force for right lift is added at the new transform for the right wing
            rb.AddForceAtPosition(rightWingTransform.up * leftWingLift, rightWingTransform.position);  
        }
    }
}
