using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireLoot : ItemEffect
{
    private ShopController shopController;

    [SerializeField] private GameObject extendFireTime;
    [SerializeField] private GameObject increaseFireDamage;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        controller.a_canFire = true;
        shopController.AddItemsInFutureSets(extendFireTime);
        shopController.AddItemsInFutureSets(increaseFireDamage);
        shopController.RemoveItemsInFutureSets(gameObject.name);
    }
}
