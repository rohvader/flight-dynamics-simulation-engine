using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // object the camera follows, assigned in the inspector
    //not predefined as the spitfire plane for scalability
    public Transform target;
    
    // offset of camera relative to the target;moves  camera up/down or closer/further 
    public Vector3 offset = new Vector3(1.5f, 10, 25);
    
    // controls how gently the camera moves towards its new position
    // smaller= more gradual transition.
    public float smoothSpeed = 0.125f;

    // lateUpdate being used so that the target has already moved for this frame.
    //camera can always updates after the target to keep it in view.
    void LateUpdate()
    {
        if (target == null) return;
        //takes plane's current position and add our offset, considering its current rotation degree
        Vector3 desiredPosition = target.position + target.rotation * offset;
        
        // instead of snapping the camera instantly to this new position, lerp function moves it smoothly
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        // making sure the camera is always looking at the target to keep it focused on the plane during motion
        transform.LookAt(target.position);
    }
}
 
