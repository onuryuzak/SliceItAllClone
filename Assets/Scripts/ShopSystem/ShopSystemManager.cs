using System.Collections;
using System.Collections.Generic;
using Onur.Template;
using UnityEngine;
using Onur.Template.UI;

public class ShopSystemManager : BaseSingleton<ShopSystemManager>
{
    [SerializeField] InventorySO _inventorySO;
    public ShopItemSO _shopItemDatas;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject shopButton;


    public int lastBoughtItemId => _inventorySO.lastBoughtSkinId;
    public int lastActiveSkinId => _inventorySO.lastActiveSkinId;

    private void OnEnable()
    {
        EventManager.LevelStarted += OnLevelStart;
        EventManager.LevelLoaded += OnLevelLoaded;
        panel.SetActive(false);
    }


    private void OnDisable()
    {
        EventManager.LevelStarted -= OnLevelStart;
        EventManager.LevelLoaded -= OnLevelLoaded;
    }

    public void ShopItemButtonClicked(ShopButtonItem buttonIDController)
    {
        for (int i = 0; i < _shopItemDatas.itemLists.Count; i++)
        {
            if (_shopItemDatas.itemLists[i].id == buttonIDController.id)
            {
                if ((buttonIDController.id - 1) < _inventorySO.lastBoughtSkinId)
                {
                    _inventorySO.lastActiveSkinId = buttonIDController.id;
                    Destroy(KnifeSpawnManager.instance.currentSkin);
                    KnifeSpawnManager.instance.SpawnKnife();
                    EventManager.OnShopItemChangeStatus();
                }
                else if ((buttonIDController.id - 1) == _inventorySO.lastBoughtSkinId)
                {
                    if (_inventorySO.money >= _shopItemDatas.itemLists[i].price)
                    {
                        _inventorySO.lastBoughtSkinId = _shopItemDatas.itemLists[i].id;
                        _inventorySO.money -= _shopItemDatas.itemLists[i].price;

                        _inventorySO.lastActiveSkinId = buttonIDController.id;
                        UIGameCanvasManager.instance.MoneyBoxText.text = "" + _inventorySO.money;
                        Destroy(KnifeSpawnManager.instance.currentSkin);
                        KnifeSpawnManager.instance.SpawnKnife();
                        Database.instance.saveGame();
                        EventManager.OnShopItemChangeStatus();
                    }
                }
            }
        }
    }

    public void OpenShopBoard()
    {
        PopupManager.instance.Show("Shop");
        //ArmController[] armControllers = FindObjectsOfType<ArmController>();
        // foreach (var item in armControllers)
        // {
        //     item.enabled = false;
        // }
        panel.SetActive(true);
    }

    public void OnClickStartGame()
    {
        PopupManager.instance.CloseActivePopup();
        ClosePanel();
        EventManager.OnLevelStarted();
        GameManager.instance.canPlay = true;
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    private void OnLevelStart()
    {
        shopButton.SetActive(false);
    }

    private void OnLevelLoaded()
    {
        shopButton.SetActive(true);
    }
}