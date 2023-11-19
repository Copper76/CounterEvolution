using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PlayerShopController : ShopController
{
    [SerializeField] private TextMeshProUGUI[] texts = new TextMeshProUGUI[3];
    [SerializeField] private GameObject keepCurrentWeapon;

    private GameObject[] itemsPresent = new GameObject[3];
    private Vector3[] itemPositions = new Vector3[3] { new Vector3(-5, 5, 0), new Vector3(0, 5, 0), new Vector3(5, 5, 0) };

    //specific tiered items
    [SerializeField] private List<GameObject> InitialWeapons;
    [SerializeField] private List<GameObject> tierOneItems;
    [SerializeField] private List<GameObject> tierTwoWeapons;
    [SerializeField] private List<GameObject> tierTwoItems;

    void Awake()
    {
        itemSet = 0;
        items = new List<GameObject>[4];
        items[0] = InitialWeapons;
        items[1] = tierOneItems;
        items[2] = tierTwoWeapons;
        items[3] = tierTwoItems;
        weaponSets = new HashSet<int>();
        weaponSets.Add(2);
    }

    void OnEnable()
    {
        if (weaponSets.Contains(itemSet))
        {
            keepCurrentWeapon.SetActive(true);
        }
        List<int> removeArr = new List<int>();
        if (items[itemSet].Count <= itemsPresent.Length)
        {
            for (int i = 0; i < items[itemSet].Count; i++)
            {
                itemsPresent[i] = Instantiate(items[itemSet][i], transform);
                itemsPresent[i].name = items[itemSet][i].name;
                itemsPresent[i].transform.position = itemPositions[i];
                if (itemsPresent[i].name.Length >= 3 && itemsPresent[i].name.Substring(0, 3) == "OTU")
                {
                    texts[i].text = itemsPresent[i].name.Substring(3, itemsPresent[i].name.Length - 8);
                    removeArr.Add(i);
                }
                else
                {
                    texts[i].text = itemsPresent[i].name.Substring(0, itemsPresent[i].name.Length - 5);
                }
            }
        }
        else
        {
            int[] picked = new int[itemsPresent.Length];
            for (int i = 0; i < itemsPresent.Length; i++)
            {
                int pickedI;
                do
                {
                    pickedI = UnityEngine.Random.Range(0, items[itemSet].Count);
                } while (picked.Contains(pickedI));
                picked[i] = pickedI;
                itemsPresent[i] = Instantiate(items[itemSet][picked[i]], transform);
                itemsPresent[i].name = items[itemSet][picked[i]].name;
                itemsPresent[i].transform.position = itemPositions[i];
                if (itemsPresent[i].name.Length >= 3 && itemsPresent[i].name.Substring(0, 3) == "OTU")
                {
                    texts[i].text = itemsPresent[i].name.Substring(3, itemsPresent[i].name.Length - 8);
                    removeArr.Add(pickedI);
                }
                else
                {
                    texts[i].text = itemsPresent[i].name.Substring(0, itemsPresent[i].name.Length - 5);
                }
            }
        }
        if (removeArr.Count > 0)
        {
            removeArr.Sort((a, b) => b.CompareTo(a));
            foreach (int i in removeArr)
            {
                items[itemSet].RemoveAt(i);
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < itemsPresent.Length; i++)
        {
            Destroy(itemsPresent[i]);
        }
        keepCurrentWeapon.SetActive(false);
    }
}
