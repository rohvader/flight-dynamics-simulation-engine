using UnityEngine;

public class SkyboxController : MonoBehaviour
{
   
    public Material flightSkybox;

  
    void Start()
    {
        RenderSettings.skybox = flightSkybox;
    }
}
