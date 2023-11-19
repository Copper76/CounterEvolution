using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBloodLustModifierLoot : ItemEffect
{
    [SerializeField] private float bloodLustModifier;

    public override void Activate(CharController controller)
    {
        controller.a_bloodLustModifier += bloodLustModifier;
    }
}
