using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnableShock : ItemEffect
{
    private ShopController shopController;

    [SerializeField] private GameObject extendShockTime;

    void Start()
    {
        shopController = transform.parent.gameObject.GetComponent<ShopController>();
    }

    public override void Activate(CharController controller)
    {
        controller.a_canShock = true;
        shopController.AddItemsInFutureSets(extendShockTime);
        shopController.RemoveItemsInFutureSets(gameObject.name);
        Destroy(gameObject);
    }
}
