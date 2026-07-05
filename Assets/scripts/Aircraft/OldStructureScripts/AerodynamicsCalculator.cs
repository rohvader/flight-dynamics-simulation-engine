/* using System;
using UnityEngine;

//[ExecuteAlways]
public class AerodynamicsCalculator : MonoBehaviour
{
    // wing area for spitfire MKVIII
    private const float WingArea = 16f;
    //spitfire mass
    private const float Mass = 3800f; 


    // sea level standard air density in kg/m^3 (for use in exponential model for air density)
    private const float SeaLevelDensity = 1.225f;
    // scale height for earth's atmosphere in meters
    private const float ScaleHeight = 8500f;
    
    // sea level temperature in kelvin, which is 288.15K or around 15°C)
    private const float SeaLevelTemperature = 288.15f;
    // standard lapse rate in K/m (value for troposphere)
    private const float LapseRate = 0.0065f;
    // sea level pressure in pascals
    private const float SeaLevelPressure = 101325f;
    // gravitational acceleration in m/s^2
    private const float g = 9.80665f;
    // specific gas constant for air in J/(kg*K)
    private const float SpecificGasConstant = 287.058f;
    
    // characteristic length in metres (in this case wing's chord length) used for reynolds number calc
    public float CharacteristicLength = 2.0f;
    // dynamic viscosity of air in kg/(m*s) (approximate value for 288.15K) (for re num calc)
    private const float DynamicViscosity = 1.7894e-5f;
    //constants for new temperature calc
    private const float MolarMass = 0.0289644f;
    private const float UniversalGasConstant = 8.3144598f;
    private const float GravitationalAcceleration = 9.80665f;


    // parameters for lift calc
    public float Cl0 = 0.075f;                  //lift coefficient at zero aoa
    public float dCl_dalpha = 0.0667f;        //lift slope (per degree)
    public float k = 0.3270465f;                  // best-fit k value from python least-squares script
    public float Re_ref = 3.4e6f;              // reference reynolds number 
     
    public float pitchOffset = 5f; 
    public Transform plane;

    void Start()
    {
        // get the rigidbody component attached to the aircraft
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("rigidbody not found on start");
        }
        SetMass();
    }

    void Update()
    {
        Debug.Log("lift force: " + GetLiftForce() + " , velocity = " + GetCurrentVelocity() + " , Cl = " + GetLiftCoefficient() + " , AoA = " + CalculateAngleOfAttack());
    }

   // getter for wing area
    public float GetWingArea()
    {
        return WingArea;
    }

    //getter for mass
    public float GetMass()
    { 
        return Mass;
    }

    //set mass to rigidbody
    public void SetMass()
    {
        rb.mass = GetMass();
    }

    // calculate and return air density based on current altitude
    public float CalculateAirDensity()
    {
        // altitude is y position of the aircraft
        float altitude = transform.position.y;
        // simplified exponential model for air density variation with altitude
        float density = SeaLevelDensity * Mathf.Exp(-altitude / ScaleHeight);
        return density;
    }

    //get method for returning the current speed of the aircraft (magnitude of velocity)
    public float GetCurrentVelocity()
    {
        if (rb != null)
        {
            return rb.velocity.magnitude;
        }
        Debug.Log("rb null in v calc");
        return 0f;
    }

       
    // returns the plane’s angle of attack in degrees
    public float CalculateAngleOfAttack()
    {
        // get the plane’s forward vector in world space
        Vector3 forward = plane.forward;
        
        //project forward vector onto the horizontal plane*
        Vector3 forwardProjected = new Vector3(forward.x, 0f, forward.z).normalized;
        
        // calculating angle (relative to the plane's axis; negative for downards pitching)
        float aoa = Vector3.SignedAngle(forwardProjected, forward, plane.right);        
        return aoa + pitchOffset;
    }   

    // calculate and return the temperature at the current altitude using the barometric formula below (and in design section)
    public float CalculateTemperature()
    {
        // altitude is the y position of the aircraft
        float altitude = transform.position.y;
        // temperature = seaLevelTemperature - (lapseRate * altitude)
        float temperature = SeaLevelTemperature - (LapseRate * altitude);
        return temperature;
    }

    //new method for air pressure calculation using more accurate exponential model
    public float CalculateAirPressure()
    {   
        float altitude = transform.position.y;
        float currentTemp = CalculateTemperature();
        float TBar = (currentTemp + SeaLevelTemperature)/2;
        float airPressure = SeaLevelPressure * Mathf.Exp((-GravitationalAcceleration * MolarMass * altitude)/(UniversalGasConstant * TBar));
        return airPressure;
    }
    
    // calculate and return the reynolds number based on current conditions
    //ratio of inertial to viscous forces acting in a fluid
    public float CalculateReynoldsNumber()
    {
        // reynolds number = (density * velocity * characteristic length) / dynamic viscosity
        float density = CalculateAirDensity();
        float velocity = GetCurrentVelocity();
        float reynolds = (density * velocity * CharacteristicLength) / DynamicViscosity;
        return reynolds;
    }

    public float GetLiftCoefficient()
    {   
        float alpha = CalculateAngleOfAttack();
        float reynolds = CalculateReynoldsNumber();
        // compute the linear part of the lift coefficient based on angle of attack
        // x(alpha) = cl0 + (dcl/dalpha)*alpha
        float xAlpha = Cl0 + dCl_dalpha * alpha;
        
        // compute the reynolds correction factor using the logarithmic term
        // factor = 1 + k * ln(reynolds / re_ref)
        float reynoldsFactor = 1.0f + k * (float)Math.Log(reynolds / Re_ref);
        
        //compute the overall lift coefficient using the full model
        //cl = x(alpha) * (1 + k * ln(reynolds / re_ref))
        float cl = xAlpha * reynoldsFactor;
        return cl;
    }
    public float GetLiftForce()
    {   
        float airDensity = CalculateAirDensity();
        float airspeed = GetCurrentVelocity(); // abstraction of equating air speed to aircraft velocity
        float wingArea = GetWingArea();
        float cl = GetLiftCoefficient();
        
        // finally find the lift force using the standard lift equation:
        // lift = 0.5 * airDensity * (airspeed^2) * wingArea * cl
        float liftForce = 0.5f * airDensity * airspeed * airspeed * wingArea * cl;
        
        return liftForce;
    }
}


 */