using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
