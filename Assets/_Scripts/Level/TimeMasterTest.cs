using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMasterTest : MonoBehaviour
{
    [Range(0, 1)] public float TimeScale = 1;

    private void OnValidate()
    {
        Time.timeScale = TimeScale;
    }
}
