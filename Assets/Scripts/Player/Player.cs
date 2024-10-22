using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action ShootInput;
    public static event Action ReloadInput;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShootInput?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadInput?.Invoke();
        }
    }
}