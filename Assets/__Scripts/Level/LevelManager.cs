using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action HeartDestroyed;

    private void Awake()
    {
        HeartDestroyed += LevelLost;
    }

    public void LevelLost()
    {
        Debug.Log("Heart destroyed");
    }
}
