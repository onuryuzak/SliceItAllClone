using System.Collections;
using System.Collections.Generic;
using Onur.Template;
using UnityEngine;
using DG.Tweening;

public class InventoryManager : BaseSingleton<InventoryManager>
{
    public InventorySO _inventorySO;

    [Header("Header")]
    [SerializeField] MoneyChangeEventSO _moneyChangeEvent;
    
    public int money => _inventorySO.money;
    
    Tween _saveTween;
    public void addMoney(int value)
    {
        _inventorySO.money += value;

        if (_inventorySO.money < 0)
            _inventorySO.money = 0;

        saveGame();

        _moneyChangeEvent.onMoneyChangeListener.Invoke(_inventorySO.money);
    }
    void saveGame()
    {
        if (_saveTween != null)
            _saveTween.Kill();

        _saveTween = DOVirtual.DelayedCall(.5f, () =>
        {
            
            Database.instance.saveGame();
            _saveTween = null;
        });
    }
}
