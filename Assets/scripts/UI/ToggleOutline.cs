using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Outline))]
public class ToggleOutline : MonoBehaviour
{
    // referencing to the Outline component of the button
    private Outline outline;

    // outline color= green & transparency = a value between 0 and 1, where values close to 0 indicate higher transparency
    private Color activeOutlineColor = new Color(0f, 1f, 0f, 35f/255f); // RGB (0, 255, 0)

    // Outline effect distance
    [SerializeField]
    private Vector2 outlineDistance = new Vector2(0, 0);

    void Awake()
    {
        // Get or add the Outline component
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        // configure the Outline component
        outline.effectColor = activeOutlineColor;
        outline.effectDistance = outlineDistance;

        // initially disable the outline
        outline.enabled = false;
    }

    //public method to toggle the outline on and off, linked to the button's Onclick event 
    public void ToggleButtonOutline()
    {
        if (outline != null)
        {
            outline.enabled = !outline.enabled;
        }
        else
        {
            Debug.LogWarning("outline component null");
        }
    }
    public bool IsOutlineEnabled()
    {
        return outline.enabled;
    }
}
