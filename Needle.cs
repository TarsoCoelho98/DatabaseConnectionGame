using Assets.Scripts.Persistence;
using Assets.Scripts.Persistence.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public Weapon Weapon;

    // Start is called before the first frame update
    void Start()
    {
        Weapon = GameCodeDataSource.Instance.WeaponDAO.GetWeapon(2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            var player = other.GetComponent<Player>();
            player.UpdateHealth(Weapon.VlAttack);
        }
    }
}
