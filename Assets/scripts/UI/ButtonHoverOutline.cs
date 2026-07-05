using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]  //ensures element has a button component
public class ButtonHoverOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outline;
    private Color activeOutlineColor = new Color(183f/255f, 183f/255f, 183f/255f, 30f/255f); // RGB (183,183,183) Hex: B7B7B7

    [SerializeField]
    private Vector2 outlineDistance = new Vector2(0, 0);

    void Awake()
    {   
        outline = GetComponent<Outline>();  // retrieves outline component from button
        outline.effectColor = activeOutlineColor;
        outline.effectDistance = outlineDistance;
        if (outline == null)
        {
            Debug.LogError("outline component missing for the " + gameObject.name); // for testing (output when the outline is missing)
        }

    }

    // called when the pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {   
        if (outline.enabled == false)
        {
            HoverColour();
        }
        
    }

    public void HoverColour()
    {
        //outline = gameObject.AddComponent<Outline>();
        outline.effectColor = activeOutlineColor; // sets to grey instead of green
        outline.enabled = true;
    }
    // called when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}


