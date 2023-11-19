using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoisonTimeLoot : ItemEffect
{
    [SerializeField] private float durationModifier;

    public override void Activate(CharController controller)
    {
        controller.a_poisonDuration += durationModifier;
    }
}
