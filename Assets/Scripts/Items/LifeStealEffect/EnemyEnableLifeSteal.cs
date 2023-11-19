using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnableLifeSteal : ItemEffect
{
    private ShopController shopController;

    [SerializeField] private GameObject increaseLifeSteal;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        controller.a_canLifeSteal = true;
        shopController.AddItemsInFutureSets(increaseLifeSteal);
        shopController.RemoveItemsInFutureSets(gameObject.name);
        Destroy(gameObject);
    }
}
