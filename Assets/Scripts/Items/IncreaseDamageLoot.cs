using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageLoot : ItemEffect
{
    [SerializeField] private float damageModifier;

    public override void Activate(CharController controller)
    {
        controller.damageModifier += damageModifier;
    }
}
