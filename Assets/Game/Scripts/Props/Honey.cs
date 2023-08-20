using System;
using UnityEngine;

public class Honey
{
    private const int _MIN_VALUE = 0;
    private const int _MAX_VALUE = 999;

    private int _current_value;
    private int _record_value;

    public event Action<int, int> HoneyChangedEvent;

    public void Initialize()
    {
        _current_value = _MIN_VALUE;
        _record_value = _MIN_VALUE;

        OnHoneyChanged();
    }

    public void Increment(int amount)
    {
        _current_value += amount;
        _current_value = Mathf.Clamp(_current_value, _MIN_VALUE, _MAX_VALUE);
        
        if (_current_value > _record_value)
        {
            _record_value = _current_value;
        }

        OnHoneyChanged();
    }

    public void Restore()
    {
        _current_value = _MIN_VALUE;
        OnHoneyChanged();
    }

    private void OnHoneyChanged()
    {
        HoneyChangedEvent?.Invoke(_current_value, _record_value);
    }
}