/* using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class RestingAoACheck : MonoBehaviour
{
    private void Update()
    {
        //measures the angle between transform.forward and its projection onto the horizontal plane
        Vector3 forwardOnGround = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        float restingAoA = Vector3.Angle(transform.forward, forwardOnGround);

        // logs alpha in the console
        Debug.Log("Resting AoA: " + restingAoA);
    }

    /* private void OnDrawGizmos()
    {
        // fidning where to place the label (2 units above the position of the plane)
        Vector3 labelPos = transform.position + Vector3.up * 2f;

        //finding the spitfire's forward direction projected onto the horizontal plane
        Vector3 forwardOnGround = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        //angle between the plane's forward direction and its horizontal projection
        float restingAoA = Vector3.Angle(transform.forward, forwardOnGround);

        //draws the resting aoa as a label in the scene view for exact reference (without needing to enter runtime)
        #if UNITY_EDITOR
        Handles.Label(labelPos, "Resting AoA: " + restingAoA);
        #endif
    } 
}
 */
