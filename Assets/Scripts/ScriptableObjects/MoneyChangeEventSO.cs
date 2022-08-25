using System.Collections;
using System.Collections.Generic;
using Onur.Template;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MoneyChangeEvent", menuName = "OnurTemplate/Event/MoneyChangeEvent")]
public class MoneyChangeEventSO : ScriptableObject
{
    public UnityEvent<int> onMoneyChangeListener;
}
