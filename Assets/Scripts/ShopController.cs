using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    protected List<GameObject>[] items;
    protected int itemSet;
    protected HashSet<int> weaponSets;

    public void IncrementSet()
    {
        if (++itemSet >= items.Length)
        {
            itemSet = items.Length - 1;
        }
    }

    public void RemoveItemsInFutureSets(string removeItemName)
    {
        for (int i = itemSet+1; i < items.Length; i++)
        {
            if (!weaponSets.Contains(i))
            {
                for (int j=0; j < items[i].Count; j++)
                {
                    if (items[i][j].name == removeItemName)
                    {
                        items[i].RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }

    public void RemoveItemInCurrentSet(string removeItemName)
    {
        for (int i = 0; i < items[itemSet].Count; i++)
        {
            if (items[itemSet][i].name == removeItemName)
            {
                items[itemSet].RemoveAt(i);
                break;
            }
        }
    }

    public void AddItemsInNextSet(GameObject addingItem)
    {
        for (int i = itemSet+1; i < items.Length; i++)
        {
            if (!weaponSets.Contains(i))
            {
                items[i].Add(addingItem);
                break;
            }
        }
    }

    public void AddItemsInFutureSets(GameObject addingItem)
    {
        for (int i = itemSet; i < items.Length; i++)
        {
            if (!weaponSets.Contains(i))
            {
                items[i].Add(addingItem);
            }
        }
    }

    public void AddWeaponInNextSet(GameObject addingWeapon)
    {
        for (int i = itemSet+1; i < items.Length; i++)
        {
            if (weaponSets.Contains(i))
            {
                items[i].Add(addingWeapon);
                break;
            }
        }
    }
    //TODO: UPGRADE IDEA:
    //SPEED UP BULLET
    //SPEED UP PLAYER
    //MORE HP
    //MORE HP REGAIN
    //MORE WEAPON
    //FASTER FIRE RATE
}
