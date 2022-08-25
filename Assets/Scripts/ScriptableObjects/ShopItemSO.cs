using System.Collections;
using System.Collections.Generic;
using Onur.Template;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "OnurTemplate/ShopItemData")]
public class ShopItemSO : BaseScriptableObject
{
    public List<itemList> itemLists;
}
[System.Serializable]
public class itemList
{
    public GameObject prefab;
    public int id;
    public int price;
}
