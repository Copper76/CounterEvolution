using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceShieldDelayLoot : ItemEffect
{
    private ShopController shopController;
    [SerializeField] private float shieldCDModifier;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        ((PlayerController)controller).shieldRegenCD -= shieldCDModifier;
        if (((PlayerController)controller).shieldRegenCD <= 1f) 
        {
            shopController.RemoveItemInCurrentSet(gameObject.name);
            shopController.RemoveItemsInFutureSets(gameObject.name);
        }
    }
}
