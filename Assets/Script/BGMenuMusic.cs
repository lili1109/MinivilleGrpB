using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMenuMusic : MonoBehaviour
{
    private static BGMenuMusic instance = null;

    public static BGMenuMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}