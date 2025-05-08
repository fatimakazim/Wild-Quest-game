using UnityEngine;

public class ShowInstructions : MonoBehaviour
{
    public GameObject instructionsPanel;

    public void ShowPanel()
    {
        instructionsPanel.SetActive(true);
    }

    public void HidePanel()
    {
        instructionsPanel.SetActive(false);
    }
}