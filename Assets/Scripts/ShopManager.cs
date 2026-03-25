using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private bool isShopOpen = false;

    [SerializeField] private GameObject shopUI;

    private void Awake()
    {
        Instance = this;
        shopUI.SetActive(false);
    }

    public void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        
        if (isShopOpen)
        {
            shopUI.SetActive(true);
            //Time.timeScale = 0f;
        }
        else
        {
            shopUI.SetActive(false);
            //Time.timeScale = 1f;
        }
    }
}
