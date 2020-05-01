using Assets.Scripts.Persistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var weapon = GameCodeDataSource.Instance.WeaponDAO.GetWeapon(1);
        var nm = weapon.NmWeapon;

        if (weapon != null)
            print(nm);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
