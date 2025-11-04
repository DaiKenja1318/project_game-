using UnityEngine;

public class BagButtonController : MonoBehaviour
{
    public GameObject panelInventory; // tham chiếu tới panel túi đồ
    private bool isOpen = false;      // trạng thái mở/đóng

    public void ToggleBag()
    {
        isOpen = !isOpen;
        panelInventory.SetActive(isOpen);
    }
}
