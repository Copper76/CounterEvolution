using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoot : ItemEffect
{
    [SerializeField] private GameObject weapon;

    public override void Activate(CharController controller)
    {
        GameObject p = Instantiate(weapon, Vector3.zero, weapon.transform.rotation);
        p.name = weapon.name;
        controller.EquipWeapon(p);
    }
}
