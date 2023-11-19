using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShieldBreakLoot : ItemEffect
{
    [SerializeField] private float SBModifier;

    public override void Activate(CharController controller)
    {
        controller.shieldBreakMultiplier += SBModifier;
    }
}
