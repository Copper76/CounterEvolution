using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireDamageLoot : ItemEffect
{
    [SerializeField] private int damageModifier;

    public override void Activate(CharController controller)
    {
        controller.a_fireDamage += damageModifier;
    }
}
