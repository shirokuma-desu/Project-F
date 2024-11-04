using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action ShootEvent;
    public static event Action ReloadEvent;
    public static event Action SpecialShootEvent;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.getFireInput)
        {
            ShootEvent?.Invoke();
        }
        else if (InputManager.Instance.getReloadInput)
        {
            ReloadEvent?.Invoke();
        }
        else if (InputManager.Instance.getSpecialInput)
        {
            SpecialShootEvent?.Invoke();
        }
    }
}