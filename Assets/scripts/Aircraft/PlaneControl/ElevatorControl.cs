using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    // current elevator deflection in degrees. positive deflection pitches nose up
    public float elevatorDeflection;
    // sensitivity factor converting input to elevator deflection
    public float elevatorSensitivity = 0.5f;

    public float deflectionDegrees = 0;


    public float minDeflection = -35f;
    public float maxDeflection = 35f;

    // transform for each elevator control surface's pivot point object
    public Transform leftElevatorPivot;
    public Transform rightElevatorPivot;

    // set the elevator deflection and update the transform's rotation
    public void SetElevatorDeflection(float deflectionDegrees)
    {   
        //one deflection value which is applied to both elevators simultaenously 
        //clamping angle of deflection between +-15 degrees 
        elevatorDeflection = Mathf.Clamp(deflectionDegrees, minDeflection, maxDeflection); 
        
        // retrieve the elevator's current rotation around the x-axis (ie degree of deflection)
        Vector3 localEuler = leftElevatorPivot.localEulerAngles;
        
        //change the angle of deflection to the defined angle
        localEuler.x = deflectionDegrees;
        // apply the rotational transform 
        leftElevatorPivot.localEulerAngles = localEuler;
        rightElevatorPivot.localEulerAngles = localEuler;
    }

    // get current elevator deflection (for applying suitable torque force)
    public float GetElevatorDeflection()
    {
        return leftElevatorPivot.localEulerAngles.x;
    }
}
/* //public method for increasing deflection --> pitching up
    public void IncreaseElevatorDeflection(float deflectionStep)    //deflectionStep defined and passed in from different script
    {
        deflectionDegrees += deflectionStep;
        SetElevatorDeflection(deflectionDegrees);
    }

    //public method for decreasing deflection --> pitching down
    public void DecreaseElevatorDeflection(float deflectionStep)    //deflectionStep defined and passed in from different script
    {
        deflectionDegrees  -= deflectionStep;
        SetElevatorDeflection(deflectionDegrees);
    } */
