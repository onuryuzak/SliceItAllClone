using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void OnLevelLoadedDelegate();

    public static event OnLevelLoadedDelegate LevelLoaded;

    public delegate void OnLevelStartedDelegate();

    public static event OnLevelStartedDelegate LevelStarted;

    public delegate void OnTouchGroundDelegate();

    public static event OnTouchGroundDelegate TouchGround;

    public delegate void OnSliceObjectDelegate(SliceableObject sliceableObject);

    public static event OnSliceObjectDelegate SlicedObject;
    
    public delegate void OnLevelSuccessDelegate();
    public static event OnLevelSuccessDelegate LevelSuccess;

    public delegate void OnLevelFailedDelegate();
    public static event OnLevelFailedDelegate LevelFailed;

    public delegate void OnShopItemChangeStatusDelegate();

    public static event OnShopItemChangeStatusDelegate ShopItemChangeStatus;

    public static void OnLevelLoaded()
    {
        LevelLoaded?.Invoke();
    }

    public static void OnLevelStarted()
    {
        LevelStarted?.Invoke();
    }

    public static void OnTouchGround()
    {
        TouchGround?.Invoke();
    }

    public static void OnSliceObject(SliceableObject sliceableObject)
    {
        SlicedObject?.Invoke(sliceableObject);
    }
    public static void OnLevelSuccess()
    {
        LevelSuccess?.Invoke();
    }
    public static void OnLevelFailed()
    {
        LevelFailed?.Invoke();
    }

    public static void OnShopItemChangeStatus()
    {
        ShopItemChangeStatus?.Invoke();
    }
    
    
    
}