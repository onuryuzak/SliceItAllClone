using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Onur.Template;

public class ShopButtonItem : MonoBehaviour
{
    public int id;
    public Text priceText;
    public Text selectStatus;
    public Image lockImage;
    public Image priceImage;
    public ShopItemSO shopItemSO;
   
    private void OnEnable()
    {
        EventManager.ShopItemChangeStatus += UpdateUI;
    }

    private void OnDisable()
    {
        EventManager.ShopItemChangeStatus -= UpdateUI;
    }
    private void Start()
    {
        UpdateUI();
        if (id == 0 && ShopSystemManager.instance.lastBoughtItemId ==0)
        {
            selectStatus.text = "SELECTED";
            selectStatus.color = Color.green;
            return;
        }
        
    }
    
    public void UpdateUI()
    {
        if (lockImage != null)
        {
            lockImage.gameObject.SetActive(false);
        }
        if (priceImage != null)
        {
            priceImage.gameObject.SetActive(false);
        }

        if (priceText != null)
            priceText.text = "";
        if (selectStatus != null)
            selectStatus.text = "";
        
        if ((id - 1) < ShopSystemManager.instance.lastBoughtItemId)
        {
            if (id == ShopSystemManager.instance.lastActiveSkinId)
            {
                selectStatus.text = "SELECTED";
                selectStatus.color = Color.green;

            }
            else
            {
                selectStatus.text = "SELECT";
                selectStatus.color = Color.white;
            }
        }
        else if ((id - 1) == ShopSystemManager.instance.lastBoughtItemId)
        {
            foreach (var item in shopItemSO.itemLists)
            {
                if (item.id == id)
                {
                    priceText.text = "" + item.price;
                }
            }
            priceText.gameObject.SetActive(true);
            priceImage.gameObject.SetActive(true);
            

        }
        else
        {
            lockImage.gameObject.SetActive(true);
        }
    }
}
