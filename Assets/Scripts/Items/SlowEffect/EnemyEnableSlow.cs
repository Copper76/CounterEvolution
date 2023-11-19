using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnableSlow : ItemEffect
{
    private ShopController shopController;

    [SerializeField] private GameObject extendSlowTime;
    [SerializeField] private GameObject increaseSlowEffect;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        controller.a_canSlow = true;
        shopController.AddItemsInFutureSets(extendSlowTime);
        shopController.AddItemsInFutureSets(increaseSlowEffect);
        shopController.RemoveItemsInFutureSets(gameObject.name);
        Destroy(gameObject);
    }
}
