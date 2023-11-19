using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSlowEffectLoot : ItemEffect
{
    [SerializeField] private float effectModifier;

    public override void Activate(CharController controller)
    {
        controller.a_slowAmount += effectModifier;
    }
}
