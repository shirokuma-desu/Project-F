using System;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    // Start is called before the first frame update
    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
        timeSinceLastSwitch = 0;
    }

    private void Select(int selectedWeaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == selectedWeaponIndex);

        }
        timeSinceLastSwitch = 0;
        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < weapons.Length; i++)
        {
            {
                weapons[i] = transform.GetChild(i);
            }
        }
        if (keys == null) keys = new KeyCode[weapons.Length];

    }

    // Update is called once per frame
    void Update()
    {
        int prevSelectedWeapon = selectedWeapon;
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
            {
                selectedWeapon = i;
            }
            if (prevSelectedWeapon != selectedWeapon) Select(selectedWeapon);

            timeSinceLastSwitch += Time.deltaTime;
        }
    }
}