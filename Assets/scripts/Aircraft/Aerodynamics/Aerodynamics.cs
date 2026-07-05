using UnityEngine;
using System;

public static class Aerodynamics
{
    // sea level standard air density in kg/m^3 (for exponential model)
    private const float SeaLevelDensity = 1.225f;
    // scale height for earth's atmosphere in metres
    private const float ScaleHeight = 8500f;
    // gravitational acceleration in m/s^2
    private const float g = 9.80665f;
    // dynamic viscosity of air in kg/(m*s) (approximate at 288.15k)
    private const float DynamicViscosity = 1.7894e-5f;
    public static float reynoldsFactor;
    public static float xAlpha;

    // calculates air density based on altitude (y position)
    public static float CalculateAirDensity(float altitude)
    {
        // simplified exponential model for density
        float density = SeaLevelDensity * Mathf.Exp(-altitude / ScaleHeight);
        return density;
    }

    // calculates the reynolds number
    public static float CalculateReynoldsNumber(SpitfireMKVIII planeData, float velocity, float airDensity)
    {
        // re = (density * velocity * characteristic length) / dynamic viscosity
        float re = (airDensity * velocity * planeData.characteristicLength) / DynamicViscosity;
        return re;
    }

    // calculates the lift coefficient
    public static float GetLiftCoefficient(SpitfireMKVIII planeData, float alpha, float reynolds)
    {
        //linear calc for the lift coefficient
        xAlpha = planeData.Cl0 + planeData.dCl_dalpha * alpha;
        //reynolds logarithmic correction factor
        reynoldsFactor = 1.0f + planeData.k * (float)Math.Log(reynolds / planeData.Re_ref);
        // combining both parts of my formula for lift coefficient
        float cl = xAlpha * reynoldsFactor;
        return cl; 
    }

    // calculates the lift force using the standard lift equation
    public static float GetLiftForce(SpitfireMKVIII planeData, float airDensity, float airspeed, float liftCoefficient)
    {
        // lift = 0.5 * airDensity * (airspeed^2) * wingArea * cl
        float liftForce = 0.5f * airDensity * airspeed * airspeed * planeData.wingArea * liftCoefficient;
        return liftForce;
    }

    // calculates the drag coefficient
    public static float GetDragCoefficient(SpitfireMKVIII planeData, float alpha)
    {
        //implementing derived quadratic model for the drag coefficient
        //using the Horner's method correction to the quadratic for efficiency
        float cd = alpha * (planeData.COEFFICIENTS[0] * alpha + planeData.COEFFICIENTS[1]) + planeData.COEFFICIENTS[2];
        return cd;
    }

    //calculates drag force (function is run in AeroCalc.cs)
    public static float GetDragForce(SpitfireMKVIII planeData, float airDensity, float airspeed, float dragCoefficient)
    {
        // drag = 0.5 * airDensity * (airspeed^2) * wingArea * cd
        float dragForce = 0.5f * airDensity * airspeed * airspeed * planeData.wingArea * dragCoefficient;
        return dragForce;
    }
}
