using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFireRateLoot : ItemEffect
{
    [SerializeField] private float fireRateModifier;

    public override void Activate(CharController controller)
    {
        controller.fireRateModifier += fireRateModifier;
    }
}
