using UnityEngine;

[System.Serializable]
public class SpitfireMKVIII
{
    // wing area for spitfire mkviii
    public float wingArea = 16f;
    // pitch offset for aoa calc (because plane model has nose tilted up initially)
    public float pitchOffset = 5f;
    // mass of the aircraft
    public float mass = 3800f;
    // characteristic length in metres (wing chord length)
    public float characteristicLength = 2.0f;
    // lift coefficient at zero aoa
    public float Cl0 = 0.075f;
    // lift slope (per degree)
    public float dCl_dalpha = 0.0667f;
    // best-fit k value from analysis
    public float k = 0.3270465f;
    // reference reynolds number
    public float Re_ref = 3.4e6f;
    //defining constant array to hold the coefficients (readonly because arrays cant be made constant + capitals for convention)
    public readonly float[] COEFFICIENTS = {0.00042673f, 0.00046591f,0.01484007f};
    public float maxSpeed = 165f;
}
