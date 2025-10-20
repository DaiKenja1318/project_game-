using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int playerMoney = 1000;
    public TMP_Text moneyText;
    public GameObject notEnoughMoneyText;

    public InventoryManager inventoryManager;

    private void Start()
    {
        UpdateMoneyUI();
        notEnoughMoneyText.SetActive(false);
    }

    public void BuyItem(ShopItem item)
    {
        if (playerMoney >= item.price)
        {
            playerMoney -= item.price;
            UpdateMoneyUI();

            inventoryManager.AddItem(item.itemImage);
        }
        else
        {
            StartCoroutine(ShowNotEnoughMoney());
        }
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = playerMoney.ToString();
    }

    private System.Collections.IEnumerator ShowNotEnoughMoney()
    {
        notEnoughMoneyText.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughMoneyText.SetActive(false);
    }
}
