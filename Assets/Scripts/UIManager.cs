using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject bagPanel;

    public void ToggleShop()
    {
        bool isActive = shopPanel.activeSelf;
        shopPanel.SetActive(!isActive);
        bagPanel.SetActive(false);
    }

    public void ToggleBag()
    {
        bool isActive = bagPanel.activeSelf;
        bagPanel.SetActive(!isActive);
        shopPanel.SetActive(false);
    }
}
