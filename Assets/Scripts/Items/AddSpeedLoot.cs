using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeedLoot : ItemEffect
{
    [SerializeField] private float speedModifier;

    public override void Activate(CharController controller)
    {
        controller.speed += speedModifier;
    }
}
