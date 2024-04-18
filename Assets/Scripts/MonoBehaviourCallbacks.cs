using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourCallbacks : MonoBehaviour
{
    public Action awake, start, update;

    private void Awake()
    {
        TryInvoke(awake);
    }

    private void Start()
    {
        TryInvoke(start);
    }

    private void Update()
    {
        TryInvoke(update);
    }

    private void TryInvoke(Action action)
    {
        if (action != null)
        {
            action();
        }
    }
}
