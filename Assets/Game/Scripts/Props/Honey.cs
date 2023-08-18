using System;
using UnityEngine;

public class Honey
{
    private const int _MIN_VALUE = 0;
    private const int _MAX_VALUE = 999;

    private int _value;

    public event Action<int> HoneyChangedEvent;

    public void Increment(int amount)
    {
        _value += amount;
        _value = Mathf.Clamp(_value, _MIN_VALUE, _MAX_VALUE);
        
        OnHoneyChanged();
    }

    public void Restore()
    {
        _value = _MIN_VALUE;

        OnHoneyChanged();
    }

    private void OnHoneyChanged()
    {
        HoneyChangedEvent?.Invoke(_value);
    }
}