using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBloodLustLoot : ItemEffect
{
    private ShopController shopController;

    [SerializeField] private GameObject increaseBloodLustModifier;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        controller.a_canBloodLust = true;
        shopController.AddItemsInFutureSets(increaseBloodLustModifier);
        shopController.RemoveItemsInFutureSets(gameObject.name);
    }
}
