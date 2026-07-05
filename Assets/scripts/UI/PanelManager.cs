using UnityEngine;

public class ManualPanelManager : MonoBehaviour
{
    // Reference to the Home Panel
    [SerializeField]
    private GameObject MainCanvasPanel;

    // Reference to the Settings Panel
    [SerializeField]
    private GameObject PlaneSelectionPanel;

    // Reference to the Profile Panel
    [SerializeField]
    private GameObject SettingsPanel;


    void Start()
    {
        // Deactivate all panels first
        DeactivateAllPanels();

        // Activate the specified panel
        MainCanvasPanel.SetActive(true);
    }


    /// deactivates all panels by setting their GameObjects to inactive.
   
    private void DeactivateAllPanels()
    {
       
        PlaneSelectionPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        
    }
}

