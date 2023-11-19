using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLifeStealAmountLoot : ItemEffect
{
    [SerializeField] private int lifeStealModifier;

    public override void Activate(CharController controller)
    {
        controller.a_lifeStealAmount += lifeStealModifier;
    }
}
