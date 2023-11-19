using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFleshBreakLoot : ItemEffect
{
    [SerializeField] private float FBModifier;

    public override void Activate(CharController controller)
    {
        controller.fleshDamageMultiplier += FBModifier;
    }
}
