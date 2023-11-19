using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthRegenLoot : ItemEffect
{
    [SerializeField] private int regenModifier;

    public override void Activate(CharController controller)
    {
        ((PlayerController) controller).roundHeal += regenModifier;
    }
}
