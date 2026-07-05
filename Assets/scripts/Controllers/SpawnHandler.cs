using UnityEngine;

public class SpitfireReset : MonoBehaviour
{
    // stores the initial position as a position vector
    [Tooltip("Spawn")]
    public Vector3 spawnPoint = new Vector3 (100f,0f,230f);

    void Awake()
    {
        //finds starting position of plane once the scene loads.
        //initialPosition = transform.position;

        ResetPosition();
    }

    //method for resetting position
    public void ResetPosition()
    {
        transform.position = spawnPoint;
    }
}
