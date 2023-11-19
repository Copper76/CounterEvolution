using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Linq;

public class EnemiesController : MonoBehaviour
{
    public int levelNum = 0;

    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerShopController shopController;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI finalMessage;
    [SerializeField] private Transform bulletBag;
    [SerializeField] private GameObject enemyTemplate;

    private List<EnemyController> enemies = new List<EnemyController>();
    private List<EnemyShopController> enemyShops = new List<EnemyShopController>();
    private int[] shopIncrementLevels = new int[] { 1,4,5};
    public int enemyCount;
    private Vector2 enemyOffset;
    private int deathToll;
    private float waitTime;

    // Start is called before the first frame update
    void Awake()
    {
        waitTime = 3f;
        enemyOffset = new Vector2(0f, 5f);
        AddNewEnemy();
    }

    void Start()
    {
        EndLevel();
    }

    public void StartNewLevel()
    {
        //Clean up shop
        shopController.gameObject.SetActive(false);
        //update parameters
        enemyCount = enemies.Count;
        levelNum++;
        levelText.text = "ROUND " + levelNum.ToString();
        levelText.gameObject.SetActive(true);
        float angle = 2* Mathf.PI / enemyCount;

        //Player and Enemy activation
        player.Prepare(waitTime);
        for (int i=0;i<enemies.Count;i++)
        {
            //enemy activation code
            //place enemies in reasonable locations
            enemies[i].gameObject.SetActive(true);
            enemies[i].transform.position = player.respawnPoint + new Vector3(-enemyOffset.y * Mathf.Sin(angle * i),enemyOffset.y*Mathf.Cos(angle * i),0f);
            enemies[i].Respawn(waitTime);
        }
    }

    public void AddNewEnemy()
    {
        GameObject e  = Instantiate(enemyTemplate.gameObject,transform);
        enemies.Add(e.transform.GetChild(0).GetComponent<EnemyController>());
        enemyShops.Add(e.transform.GetChild(2).GetComponent<EnemyShopController>());
        enemies[enemies.Count-1].playerTransform = player.transform;
    }

    public void RemoveEnemy()
    {
        player.IncreaseBloodLust();
        deathToll++;
        if (--enemyCount <= 0)
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        foreach (Transform t in bulletBag)
        {
            Destroy(t.gameObject);
        }
        levelText.gameObject.SetActive(false);
        //reset player location and conditions
        player.Reset();
        if (shopIncrementLevels.Contains(levelNum))
        {
            shopController.IncrementSet();
        }
        shopController.gameObject.SetActive(true);
        //Add new enemy every 5 levels
        if (levelNum % 5 == 4)
        {
            AddNewEnemy();
        }
        foreach (EnemyShopController shop in enemyShops)
        {
            //enemy upgrade code
            //change weapon every 5 levels
            /**
            List<ItemEffect> currentItems = enemyShopController.getCurrentItems(enemy.level);
            ItemEffect selected = currentItems[UnityEngine.Random.Range(0, currentItems.Count)];
            selected.Activate(enemy);
            enemy.level++;
            enemy.UpdateGameTag(selected.name.Substring(0, selected.name.Length-5));
            **/
            shop.LevelUp();
        }
    }

    public void FinalMessage()
    {
        Destroy(bulletBag.gameObject);
        if (shopController.gameObject.activeInHierarchy)
        {
            finalMessage.text = string.Format("YOU HAVE CLEARED {0} ROUNDS,\n AND ELIMINATED {1} ENEMIES", levelNum, deathToll);
        }
        else
        {
            finalMessage.text = string.Format("YOU HAVE CLEARED {0} ROUNDS,\n AND ELIMINATED {1} ENEMIES", levelNum-1, deathToll);
        }
        Destroy(shopController.gameObject);
        finalMessage.transform.parent.gameObject.SetActive(true);
    }
}
