using System.Collections;
using System.Collections.Generic;
using Onur.Template;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "OnurTemplate/Inventory")]
public class InventorySO : BaseScriptableObject
{
    public int money;
    public int lastBoughtSkinId;
    public int lastActiveSkinId;

    public override void reset()
    {
        money = 0;
        lastActiveSkinId = 0;
        lastBoughtSkinId = 0;
    }
}