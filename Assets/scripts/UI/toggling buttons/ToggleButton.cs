using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // referencing to the button's image component instead of outline
    private Image buttonImage;

    
    // 30% transparency for colours
    [SerializeField] 
    private Color normalColor = new Color(0f, 0f, 0f, 0.3f);
    [SerializeField] 
    private Color hoverColor = new Color(183f / 255f, 183f / 255f, 183f / 255f, 0.3f); 
    [SerializeField] 
    private Color toggledColor = new Color(0f, 1f, 0f, 0.3f);  
    

    // initial states are both off
    private bool isToggled = false;
    private bool isHovered = false;

    
    private ToggleButtonGroup group;

    void Awake()
    {
        buttonImage = GetComponent<Image>();

        // the initial colour is white (adjustable above)
        buttonImage.color = normalColor;

        // locates the ToggleButtonGroup in the unity scene
        group = FindObjectOfType<ToggleButtonGroup>();
        group.RegisterButton(this); // runs the method in the ToggleButtonGroup script to add the button to the group
        
    }

    // method which runs every time the user cursor hovers over the button
    public void OnPointerEnter(PointerEventData eventData)
    {   
        isHovered = true;
        UpdateColour();  //updates the colour everytime the user's cursor enters the button area
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        UpdateColour();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isToggled) // if the button is in the untoggled state
        {
            // notifies the group that the button is meant to be toggled on
            group.OnButtonToggled(this);
            SetToggled(true);
        }
        else
        {
            // allows toggling off
            SetToggled(false);
        }
    }

    // method for setting the toggle state based on the value true/false passed into the procedure
    public void SetToggled(bool toggle)
    {
        isToggled = toggle;
        UpdateColour();
    }

    private void UpdateColour() // 
    {
        if (isHovered)
        {
            if (isToggled)
            {
                buttonImage.color = toggledColor;
            }
            else
            {
                buttonImage.color = hoverColor;
            }
        }
        else
        {
            if (isToggled)
            {
                buttonImage.color = toggledColor;
            }
            else
            {
                buttonImage.color = normalColor;
            }
        }
    }

    void Start()
    {
        // manually override Inspector values by setting colors explicitly on start
        normalColor = new Color(0f, 0f, 0f, 0.3f);
        hoverColor = new Color(183f / 255f, 183f / 255f, 183f / 255f, 0.3f);
        toggledColor = new Color(0f, 1f, 0f, 0.3f);
        
        buttonImage.color = normalColor;
        Debug.Log("colours set");
    } 
    
} 
 




/* using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // References to the button's Image component
    private Image buttonImage;

    // Colors with corrected alpha values (30% opacity)
    [SerializeField] private Color normalColor = new Color(0f, 0f, 0f, 0.3f); // 30% opacity
    [SerializeField] private Color hoverColor = new Color(183f / 255f, 183f / 255f, 183f / 255f, 0.3f); // 30% opacity
    [SerializeField] private Color toggledColor = new Color(0f, 1f, 0f, 0.3f); // 30% opacity

    // State
    private bool isToggled = false;
    private bool isHovered = false;

    // Reference to the ToggleButtonGroup
    private ToggleButtonGroup group;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("ToggleButton script requires an Image component on the same GameObject.");
        }

        // Initialize with normal color
        buttonImage.color = normalColor;
        Debug.Log($"[ToggleButton] Awake: Set initial color to {normalColor}");

        // Find the ToggleButtonGroup in the scene
        group = FindObjectOfType<ToggleButtonGroup>();
        if (group == null)
        {
            Debug.LogError("No ToggleButtonGroup found in the scene. Please add a ToggleButtonGroup to manage toggle buttons.");
        }
        else
        {
            group.RegisterButton(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        UpdateColor();
        Debug.Log("[ToggleButton] OnPointerEnter: Hovered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        UpdateColor();
        Debug.Log("[ToggleButton] OnPointerExit: Unhovered");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isToggled)
        {
            // Notify the group that this button is toggled on
            group.OnButtonToggled(this);
            SetToggled(true);
            Debug.Log("[ToggleButton] OnPointerClick: Toggled On");
        }
        else
        {
            // Optionally, allow toggling off
            SetToggled(false);
            // If you want one button always selected, comment out the above lines
            //Debug.Log("[ToggleButton] OnPointerClick: Already toggled on");
        }
    }

    // Method to set the toggle state
    public void SetToggled(bool toggle)
    {
        isToggled = toggle;
        UpdateColor();
        Debug.Log($"[ToggleButton] SetToggled: Set to {toggle}");
    }

    private void UpdateColor()
    {
        if (buttonImage == null) return;

        if (isHovered)
        {
            buttonImage.color = isToggled ? toggledColor : hoverColor;
            Debug.Log($"[ToggleButton] UpdateColor: Hovered - Set color to {(isToggled ? "ToggledColor" : "HoverColor")} with alpha {buttonImage.color.a}");
        }
        else
        {
            buttonImage.color = isToggled ? toggledColor : normalColor;
            Debug.Log($"[ToggleButton] UpdateColor: Not Hovered - Set color to {(isToggled ? "ToggledColor" : "NormalColor")} with alpha {buttonImage.color.a}");
        }
    }

    // Optional: Public method to get the toggle state
    public bool IsToggled()
    {
        return isToggled;
    }
} */

/* using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // References to the button's Image component
    private Image buttonImage;

    // Colors with corrected alpha values (30% opacity)
    [SerializeField] private Color normalColor = new Color(0f, 0f, 0f, 0.3f); // 30% opacity
    [SerializeField] private Color hoverColor = new Color(183f / 255f, 183f / 255f, 183f / 255f, 0.3f); // 30% opacity
    [SerializeField] private Color toggledColor = new Color(0f, 1f, 0f, 0.3f); // 30% opacity

    // State
    private bool isToggled = false;
    private bool isHovered = false;

    // Reference to the ToggleButtonGroup
    private ToggleButtonGroup group;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("ToggleButton script requires an Image component on the same GameObject.");
        }

        // Initialize with normal color
        buttonImage.color = normalColor;
        Debug.Log($"[ToggleButton] Awake: Set initial color to {normalColor}");

        // Find the ToggleButtonGroup in the scene
        group = FindObjectOfType<ToggleButtonGroup>();
        if (group == null)
        {
            Debug.LogError("No ToggleButtonGroup found in the scene. Please add a ToggleButtonGroup to manage toggle buttons.");
        }
        else
        {
            group.RegisterButton(this);
        }
    }

    void Start()
    {
        // Override Inspector values by setting colors explicitly
        normalColor = new Color(0f, 0f, 0f, 0.3f);
        hoverColor = new Color(183f / 255f, 183f / 255f, 183f / 255f, 0.3f);
        toggledColor = new Color(0f, 1f, 0f, 0.3f);
        
        // Apply the initial color
        buttonImage.color = normalColor;
        Debug.Log($"[ToggleButton] Start: Colors set with alpha values.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        UpdateColor();
        Debug.Log("[ToggleButton] OnPointerEnter: Hovered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        UpdateColor();
        Debug.Log("[ToggleButton] OnPointerExit: Unhovered");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isToggled)
        {
            // Notify the group that this button is toggled on
            group.OnButtonToggled(this);
            SetToggled(true);
            Debug.Log("[ToggleButton] OnPointerClick: Toggled On");
        }
        else
        {
            // allows toggling off
            /SetToggled(false);
            
            Debug.Log("[ToggleButton] OnPointerClick: Already toggled on");
        }
    }

    // Method to set the toggle state
    public void SetToggled(bool toggle)
    {
        isToggled = toggle;
        UpdateColor();
        Debug.Log($"[ToggleButton] SetToggled: Set to {toggle}");
    }

    private void UpdateColor()
    {
        if (buttonImage == null) return;

        if (isHovered)
        {
            buttonImage.color = isToggled ? toggledColor : hoverColor;
            Debug.Log($"[ToggleButton] UpdateColor: Hovered - Set color to {(isToggled ? "ToggledColor" : "HoverColor")} with alpha {buttonImage.color.a}");
        }
        else
        {
            buttonImage.color = isToggled ? toggledColor : normalColor;
            Debug.Log($"[ToggleButton] UpdateColor: Not Hovered - Set color to {(isToggled ? "ToggledColor" : "NormalColor")} with alpha {buttonImage.color.a}");
        }
    }

    // Optional: Public method to get the toggle state
    public bool IsToggled()
    {
        return isToggled;
    }
} */

