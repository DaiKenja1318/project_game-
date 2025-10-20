using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public ShopManager shopManager;
    public ShopItem item;

    public void Buy()
    {
        shopManager.BuyItem(item);
    }
}