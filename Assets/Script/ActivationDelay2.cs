using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivationDelay2 : MonoBehaviour
{
    [Header("Main Settings")]
    public UnityEvent StartEvent;
    public UnityEvent MainEvent;
    public float Delay;
    void Start()
    {
        StartEvent?.Invoke();
    }
    public void InvokeMainEvents()
    {
        MainEvent?.Invoke();
    }

    public void InvokeMainEventDelay()
    {
        Invoke("InvokeMainEvents", Delay);
    }

}
