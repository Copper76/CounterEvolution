using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaxHealthLoot : ItemEffect
{
    [SerializeField] private int HPModifier;

    public override void Activate(CharController controller)
    {
        controller.maxHP+=HPModifier;
    }
}
