/* using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class RestingAoACheck : MonoBehaviour
{
    private void Update()
    {
        // measure the angle between transform.forward and its projection onto the horizontal plane
        Vector3 forwardOnGround = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        float restingAoA = Vector3.Angle(transform.forward, forwardOnGround);

        // log it so you can see the value in the console (even outside play mode)
        Debug.Log("Resting AoA: " + restingAoA.ToString("F2") + "°");
    }

    private void OnDrawGizmos()
    {
        // optional: draw a label above the plane in the scene view
        Vector3 labelPos = transform.position + Vector3.up * 2f;
        Vector3 forwardOnGround = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        float restingAoA = Vector3.Angle(transform.forward, forwardOnGround);

        #if UNITY_EDITOR
        Handles.Label(labelPos, "Resting AoA: " + restingAoA.ToString("F2") + "°");
        #endif
    }
}
 */