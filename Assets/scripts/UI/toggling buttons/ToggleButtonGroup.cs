using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonGroup : MonoBehaviour
{
    // list to hold all toggle buttons in the group
    private List<ToggleButton> toggleButtons = new List<ToggleButton>();

    // Register a button to the group
    public void RegisterButton(ToggleButton button)
    {
        if (!toggleButtons.Contains(button))
        {
            toggleButtons.Add(button);
        }
    }

    // this method is called by a button when it is toggled on
    public void OnButtonToggled(ToggleButton toggledButton)
    {
        foreach (var button in toggleButtons)   // iterates through the list of buttons 
        {
            if (button != toggledButton)    // checks if the button is the one that has just been toggled 
            {
                button.SetToggled(false);   // turns off buttons other than the one that has just been toggled to on
            }
        }
    }
}
