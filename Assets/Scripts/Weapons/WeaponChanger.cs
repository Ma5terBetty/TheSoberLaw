using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    #region PROPERTIES
    private Dictionary<int, GameObject> _weaponsDic = new Dictionary<int, GameObject>();
    [Header("Weapons Scriptable Objects")]
    [SerializeField] private GameObject[] _weapons;
    #endregion

    private void Start()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weaponsDic.Add(i, _weapons[i]);
        }
    }

    public GameObject RequestWeapon(int key)
    {
        GameObject weapon;
        if (_weaponsDic.ContainsKey(key))
        {
            weapon = _weaponsDic[key];
        }
        else
        {
            Debug.LogWarning($"El diccionario no contiene ningun arma con la key {key}");
            weapon = null;
        }
        return weapon;
    }
}
