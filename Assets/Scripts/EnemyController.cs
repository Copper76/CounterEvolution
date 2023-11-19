using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : CharController
{

    public Transform playerTransform;

    [SerializeField] private TextMeshPro tagText;
    [SerializeField] private GameObject nameTag;

    private bool isAlive;
    private EnemiesController masterController;
    private Rigidbody2D rb;
    private Vector2 playerPos;
    private float nextFire;
    private Vector2 lookDir;
    private bool ready;
    private bool melee;
    private float range;
    private float activateTime;
    private Vector3 moveDir;

    //Unique Modifier
    public float fireRate = 2f;
    public int level;

    // Start is called before the first frame update
    void Awake()
    {
        masterController = transform.parent.parent.GetComponent<EnemiesController>();
        rb = GetComponent<Rigidbody2D>();
        isAlive = false;
        ready = false;
        melee = false;
        canFire = true;
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) 
        {
            if (Time.time > nextTick)
            {
                tick(Time.time);
                nextTick = Time.time + tickTime;
            }
            playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);
            if (Time.time > nextFire && canFire)
            {
                Fire();
            }
            if (melee)
            {
                Move();
            }
        }
        else if (ready && Time.time > activateTime)
        {
            nameTag.SetActive(false);
            isAlive = true;
            ready = false;
            Vector3 moveDir = new Vector3(UnityEngine.Random.Range(-1f, 2f), UnityEngine.Random.Range(-1f, 2f), 0);
            moveDir.Normalize();
            rb.velocity = moveDir * speed * (1f / d_slowAmount);
        }
    }

    void FixedUpdate()
    {
        lookDir = playerPos - rb.position;
        lookDir.Normalize();
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (weapon)
        {
            weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            TakeDamage(int.Parse(other.name), other.gameObject.GetComponent<AttackerInfo>().attackerController);
            Destroy(other.gameObject);
        }
    }

    public override void UpdateHealthStatus(int modifier)
    {
        hp += modifier;
        if (hp <= 0)
        {
            Die();
        }
        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }

    //Reactivate the enemies
    public void Respawn(float prepareTime)
    {
        nameTag.transform.position = transform.position + new Vector3(0f, 1.5f, 0f);
        nameTag.SetActive(true);
        hp = maxHP;
        ready = true;
        activateTime = Time.time + prepareTime;
        nextFire = Time.time + prepareTime + (fireRate + UnityEngine.Random.Range(-0.5f, 0.5f)) * fireRateModifier;
        playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    //Death method
    void Die()
    {
        ClearEffects();
        isAlive = false;
        this.gameObject.SetActive(false);
        masterController.RemoveEnemy();
    }

    void Move()
    {
        float distance = Vector2.Distance(rb.position, playerPos);
        if (distance > range)
        {
            rb.velocity = lookDir * speed * (1f / d_slowAmount);
        }
    }

    void Fire()
    {
        weaponController.Fire(angle, fireRateModifier);
        nextFire = Time.time + (fireRate+UnityEngine.Random.Range(-0.5f, 0.5f)) * fireRateModifier;
    }

    public void UpdateGameTag(string itemName)
    {
        tagText.text = string.Format("LV:{0} {1}", level, itemName);
    }

    public override void EquipWeapon(GameObject newWeapon)
    {
        if (weapon != null)
        {
            Destroy(weapon);
        }
        weapon = newWeapon;
        weapon.transform.parent = transform;
        weapon.transform.localPosition = weaponOffset;
        weapon.transform.rotation *= transform.rotation;
        weaponController = weapon.GetComponent<WeaponController>();
        melee = weaponController.melee;
        range = weaponController.range;
        weaponController.bulletBag = GameObject.Find("BulletBag").transform;
    }

    public override void TakeDamage(int rawDamage,CharController attacker)
    {
        //Calculate Damage
        int damage = (int)(rawDamage * (attacker.damageModifier + attacker.d_bloodLustModifier) * attacker.fleshDamageMultiplier);
        UpdateHealthStatus(-1 * damage);

        //Apply effects
        ApplyEffects(attacker, Time.time);
    }

    void tick(float currentTime)
    {
        if (currentTime < d_fireEndTime)
        {
            UpdateHealthStatus(-1 * d_fireDamage);
        }

        if (currentTime < d_poisonEndTime)
        {
            UpdateHealthStatus(-1 * d_poisonDamage);
        }

        if (currentTime > d_shockEndTime)
        {
            canFire = true;
        }
    }
}
