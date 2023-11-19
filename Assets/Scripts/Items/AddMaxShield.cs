using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaxShield : ItemEffect
{
    [SerializeField] private int shieldModifier;

    public override void Activate(CharController controller)
    {
        ((PlayerController)controller).maxShield += shieldModifier;
    }
}
