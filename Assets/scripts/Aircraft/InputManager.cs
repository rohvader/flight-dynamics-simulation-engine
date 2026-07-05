using UnityEngine;

public class InputManager : MonoBehaviour
{
    // reference to the aircraft physics script
    public ThrustApplication thrustApplication;
    public TestFunctions testFunctions;
    public WingControl wingControl;
    public LiftApplication liftApplication;
    public ElevatorControl elevatorControl;
    public float aileronDeflectionStep = 2f;
    public float elevatorDeflectionStep = 2f;

    void Update()
    {   
        TestFunctions();

        // check keyboard input for thrust control: 'w' to increase, 's' to decrease
        if (Input.GetKeyDown(KeyCode.Return))
        {   
            //Debug.Log("ENTER pressed");
            thrustApplication.IncreaseThrust();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("spacebar pressed");
            thrustApplication.IncreaseThrust();
        }
        
        // check keyboard input for aileron control: 'a' to roll left, 'd' to roll right
        if (liftApplication != null)
        {   
            //Debug.Log("liftapplication not null");
            if (Input.GetKey(KeyCode.A))
            {
                // roll left: increase left aileron deflection, decrease right
                liftApplication.leftWingObj.aileronDeflection -= aileronDeflectionStep * Time.deltaTime;
                liftApplication.rightWingObj.aileronDeflection += aileronDeflectionStep * Time.deltaTime;
                //Debug.Log("a pressed");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                // roll right: decrease left aileron deflection, increase right
                liftApplication.leftWingObj.aileronDeflection += aileronDeflectionStep * Time.deltaTime;
                liftApplication.rightWingObj.aileronDeflection -= aileronDeflectionStep * Time.deltaTime;
            }
            //applying changes to deflection quantities to physcial plane body 
            liftApplication.leftWingObj.SetAileronDeflection(liftApplication.leftWingObj.aileronDeflection);
            liftApplication.rightWingObj.SetAileronDeflection(liftApplication.rightWingObj.aileronDeflection);
        }


        if (elevatorControl != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                elevatorControl.deflectionDegrees -= elevatorDeflectionStep * Time.deltaTime;
                elevatorControl.SetElevatorDeflection(elevatorControl.deflectionDegrees);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                elevatorControl.deflectionDegrees += elevatorDeflectionStep * Time.deltaTime;
                elevatorControl.SetElevatorDeflection(elevatorControl.deflectionDegrees);
            }
        }

    }


    void TestFunctions()
    {
        if (Input.GetKeyDown(KeyCode.C)) //decrease altitude
        {
            testFunctions.DecreaseAltitude();
        }
        if (Input.GetKeyDown(KeyCode.F)) //decrease altitude
        {
            testFunctions.IncreaseAltitude();
        }

        //pitching
        if (Input.GetKeyDown(KeyCode.S)) //pitch up
        {
            testFunctions.PitchUp();
        }

        if (Input.GetKeyDown(KeyCode.W))    //pitch down
        {
            testFunctions.PitchDown();
        }

        // yawing
        if (Input.GetKeyDown(KeyCode.E))    //yaw right
        {
            testFunctions.YawRight();
        }

        if (Input.GetKeyDown(KeyCode.Q))    //yaw left
        {
            testFunctions.YawLeft();
        }
        
    }
}

