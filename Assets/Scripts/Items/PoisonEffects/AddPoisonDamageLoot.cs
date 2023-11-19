using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoisonDamageLoot : ItemEffect
{
    [SerializeField] private int damageModifier;

    public override void Activate(CharController controller)
    {
        controller.a_poisonDamage += damageModifier;
    }
}
