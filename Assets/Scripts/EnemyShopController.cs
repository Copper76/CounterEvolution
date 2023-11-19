using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShopController : ShopController
{
    EnemyController enemyController;

    private int[] setIncrementLevels = new int[] { 1, 4, 5 };

    //specific tiered items
    [SerializeField] private List<GameObject> InitialWeapons;
    [SerializeField] private List<GameObject> tierOneItems;
    [SerializeField] private List<GameObject> tierTwoWeapons;
    [SerializeField] private List<GameObject> tierTwoItems;

    void Awake()
    {
        enemyController = transform.parent.GetChild(0).GetComponent<EnemyController>();
        itemSet = 0;
        items = new List<GameObject>[4];
        items[0] = InitialWeapons;
        items[1] = tierOneItems;
        items[2] = tierTwoWeapons;
        items[3] = tierTwoItems;
        weaponSets = new HashSet<int>();
        weaponSets.Add(2);
    }

    public void LevelUp()
    {
        int level = enemyController.level;
        //fetch and use the item
        List<GameObject> currentItems = items[itemSet];
        GameObject selected = currentItems[UnityEngine.Random.Range(0, currentItems.Count)];
        string name;
        if (selected.name.Length >= 3 && selected.name.Substring(0, 3) == "OTU")
        {
            name = selected.name.Substring(3, selected.name.Length - 8);
            currentItems.Remove(selected);
        }
        else
        {
            name = selected.name.Substring(0, selected.name.Length - 5);
        }

        //Activate Item
        selected.GetComponent<ItemEffect>().Activate(enemyController);

        //display and clean up
        enemyController.level++;
        enemyController.UpdateGameTag(name);
        if (setIncrementLevels.Contains(enemyController.level))
        {
            IncrementSet();
        }
    }
}
