using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Aim: Manage the outline color of a button based on hover and toggle states.
/// - Grey outline on hover
/// - Green outline on toggle
/// </summary>
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Outline))]
public class ButtonOutlineManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   
    private Outline outline;

    private Color greyColor = new Color(183f / 255f, 183f / 255f, 183f / 255f, 30f / 255f); // Hex: B7B7B7
    private Color greenColor = new Color(0f, 1f, 0f, 30f / 255f); // Hex: 00FF00

    private bool isHovered = false;
    private bool isToggled = false;
   
    private Button button;

   
    void Awake()
    {
        // retrieve the Outline component
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogError($"outline component not on {gameObject.name}");
            return;
        }

        outline.effectColor = greyColor; // Default color (grey)
        outline.effectDistance = new Vector2(0,0); // adjustable distance
        outline.enabled = false; // Initially disabled

        // retrieves the Button component and adds a listener for clicks
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleOutlineState);
        }
        else
        {
            Debug.LogError($"no button on {gameObject.name}");
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        UpdateOutline();
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        UpdateOutline();
    }

 
    /// toggles the outline state between active (green) and inactive.
    private void ToggleOutlineState()
    {
        isToggled = !isToggled;
        UpdateOutline();
    }


    /// updates the outline color and enabled state based on the current hover and toggle states previously found
    private void UpdateOutline()
    {
        if (isToggled)
        {
            // If toggled on, set the outline to green and enable it
            outline.effectColor = greenColor;
            outline.enabled = true;
        }
        else if (isHovered)
        {
            // if not toggled and hovered, set the outline to grey and enable it
            outline.effectColor = greyColor;
            outline.enabled = true;
        }
        else
        {
            // if neither toggled nor hovered, disable the outline
            outline.enabled = false;
        }
    }
}
