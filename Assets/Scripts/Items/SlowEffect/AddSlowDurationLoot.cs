using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSlowDurationLoot : ItemEffect
{
    [SerializeField] private float durationModifier;

    public override void Activate(CharController controller)
    {
        controller.a_slowDuration += durationModifier;
    }
}
