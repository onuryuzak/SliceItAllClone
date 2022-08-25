using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onur.Template;
using DG.Tweening;
using Cinemachine;
public class KnifeSpawnManager : BaseSingleton<KnifeSpawnManager>
{
    [SerializeField] Transform _playerSpawnPoint;
    [SerializeField] InventorySO _inventorySO;
    [SerializeField] ShopItemSO shopItemSO;
    public GameObject currentSkin;
    [SerializeField] private CinemachineVirtualCamera _vcam;
    private void OnEnable()
    {
        
        EventManager.LevelLoaded += OnLevelLoaded;
    }


    private void OnDisable()
    {
        EventManager.LevelLoaded -= OnLevelLoaded;
    }

    public void SpawnKnife()
    {
        foreach (var item in shopItemSO.itemLists)
        {
            if (item.id == _inventorySO.lastActiveSkinId)
            {
                currentSkin = Instantiate(item.prefab, _playerSpawnPoint.position, item.prefab.transform.rotation);
                _vcam.m_Follow = currentSkin.transform;
                Database.instance.saveGame();
            }
        }
    }

    private void OnLevelLoaded()
    {
        Destroy(currentSkin);
        DOVirtual.DelayedCall(0.001f, () => SpawnKnife());
    }
}