using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseShockDurationLoot : ItemEffect
{
    [SerializeField] private float durationModifier;

    public override void Activate(CharController controller)
    {
        controller.a_shockDuration += durationModifier;
    }
}
