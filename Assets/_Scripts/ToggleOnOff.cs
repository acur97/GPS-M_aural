using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleOnOff : MonoBehaviour
{
    public UnityEvent On;
    public UnityEvent Off;

    private void Awake()
    {
        Toggle(true);
    }

    public void Toggle (bool _on)
    {
        if (_on)
        {
            On.Invoke();
        }
        else
        {
            Off.Invoke();
        }
    }
}