using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoisonLoot : ItemEffect
{
    private ShopController shopController;

    [SerializeField] private GameObject extendPoisonTime;
    [SerializeField] private GameObject increasePoisonDamage;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        controller.a_canPoison = true;
        shopController.AddItemsInFutureSets(extendPoisonTime);
        shopController.AddItemsInFutureSets(increasePoisonDamage);
        shopController.RemoveItemsInFutureSets(gameObject.name);
    }
}
