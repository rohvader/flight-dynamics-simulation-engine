using UnityEngine;
using System;

[System.Serializable]
public class WingControl
{   
    // current aileron deflection (degrees); positive increases local aoa, negative decreases
    public float aileronDeflection;
    // sensitivity factor converting deflection to a change in aoa (degrees per degree deflection)
    public float aileronSensitivity = 0.5f;
    public Transform leftAileronPivot;
    public Transform rightAileronPivot;
    private string aileronPivotSide;
    public float leftDeflect;
    public float rightDeflect;
    public AeroCalculator aeroCalc;
    private Rigidbody rb;

    //aoa before aileron modification
    public float get_global_AoA()
    {
        return aeroCalc.CalculateAngleOfAttack();
    }

    //  effective aoa for this wing
    public float get_effective_aoa(float globalAoA, float aileronDeflection)
    {
        // adjusts global aoa by the deflection effect
        float effectiveAoA = globalAoA + (aileronDeflection * aileronSensitivity);
        return effectiveAoA;
    }

    public float get_leftAileronDeflection()
    {   
        leftDeflect = leftAileronPivot.localEulerAngles.x;
        if (180<=leftDeflect && 360>=leftDeflect)
        {
            leftDeflect-=360;
        }
        return leftDeflect;
    }
    public float get_rightAileronDeflection()
    {
        rightDeflect = rightAileronPivot.localEulerAngles.x;
        if (180<=rightDeflect && 360>=rightDeflect)
        {
            rightDeflect-=360;
        }
        return rightDeflect;
    }

    public void SetAileronPivotSide(string rightOrLeftWing)
    {
        aileronPivotSide = rightOrLeftWing;
    }

    public void SetAileronDeflection(float deflectionDegrees)
    {   
        if (aileronPivotSide == "left")
        {
            SetLeftAileronDeflection(deflectionDegrees);
        }
        else if (aileronPivotSide == "right")
        {
            SetRightAileronDeflection(deflectionDegrees);
        }
    }

    public void SetLeftAileronDeflection(float deflectionDegrees)
    {
        //get current local rotation of new pivot gameobject
        Vector3 localEuler = leftAileronPivot.localEulerAngles; 

        // set the x-axis rotation to the updated deflection angle.
        localEuler.x = deflectionDegrees; 

        // apply the updated rotation 
        leftAileronPivot.localEulerAngles = localEuler; 
    }

    public void SetRightAileronDeflection(float deflectionDegrees)
    {
        //get current local rotation of new pivot gameobject
        Vector3 localEuler = rightAileronPivot.localEulerAngles; 

        // set the x-axis rotation to the updated deflection angle.
        localEuler.x = deflectionDegrees; 

        // apply the updated rotation 
        rightAileronPivot.localEulerAngles = localEuler; 
    }
}


/*     //attributes related to stalling:
    [Header("Stall attributes")]
    // stall threshold angle (beyond which lift decreases)
    public float stallAngle = 15f;
    // multiplier for lift when in stall condition (e.g. 0.5 means lift is reduced to 50%)
    public float stallReductionFactor = 0.5f; */

    /* // returns a modifier to the lift coefficient if the wing is stalled
public float get_stall_modifier(float effectiveAoA)
{
    // if absolute effective aoa exceeds stall threshold, reduce lift coefficient
    if (Mathf.Abs(effectiveAoA) > stallAngle)
    {
        return stallReductionFactor;
    }
    return 1f;
} */