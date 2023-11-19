using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class PlayerController : CharController
{
    public Vector3 respawnPoint;

    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private Slider shieldBar;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private EnemiesController masterController;

    private Vector2 displacement;
    private Rigidbody2D rb;
    private Vector2 mousePos;
    private bool canMove;
    private bool ready;
    private bool inRound;
    private float activateTime;
    private int shield;
    private float nextCharge;
    private bool autofire;

    //Unique Modifiers
    public int roundHeal;
    public int maxShield;
    public float shieldRegenCD;
    public float shieldRegenSpeed;

    private AsyncOperation asyncLoad;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        hp = maxHP;
        shield = maxShield;
        roundHeal = 5;
        nextCharge = 0f;
        canMove = true;
        canFire = false;
        ready = false;
        inRound = false;
        autofire = false;
        UpdateHealthStatus(0);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Die();
        }
        if (ready && Time.time>activateTime)
        {
            canFire = true;
            canMove = true;
            ready = false;
            inRound = true;
        }
        if (Time.time > nextTick && inRound)
        {
            tick(Time.time);
            nextTick = Time.time + tickTime;
        }
        if (Time.time > nextCharge)
        {
            UpdateShieldStatus(1);
            nextCharge = Time.time + shieldRegenSpeed;

        }
        if (autofire && canFire && weapon != null)
        {
            weaponController.Fire(angle, fireRateModifier);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = displacement * speed * selfSlow * (1f/d_slowAmount);
        }
        Vector2 lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (weapon)
        {
            weapon.transform.rotation = Quaternion.Euler(0f,0f,angle+90f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.tag == "Enemy Bullet")
        {
            TakeDamage(int.Parse(other.name), other.gameObject.GetComponent<AttackerInfo>().attackerController);
            Destroy(other.gameObject);
        }

        if (other.tag == "Loot")
        {
            other.gameObject.GetComponent<ItemEffect>().Activate(this);
            masterController.StartNewLevel();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        displacement = context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && canFire && weapon != null)
        {
            weaponController.Fire(angle, fireRateModifier);
            if (weaponController.canAutofire)
            {
                autofire = true;
            }
        }
    }

    public void Release(InputAction.CallbackContext context)
    {
        if (context.performed && canFire && weapon != null)
        {
            if (weaponController.canAutofire)
            {
                autofire = false;
            }
            weaponController.Release(angle, fireRateModifier);
        }
    }

    public void AltFire(InputAction.CallbackContext context)
    {
        if (context.performed && canFire && weapon != null)
        {
            weaponController.AltFire(angle, fireRateModifier);
        }
    }

    public void AltRelease(InputAction.CallbackContext context)
    {
        if (context.performed && canFire && weapon != null)
        {
            weaponController.AltRelease(angle, fireRateModifier);
        }
    }

    public void Dir(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public override void UpdateHealthStatus(int modifier)
    {
        if (modifier < 0)
        {
            nextCharge = Time.time + shieldRegenCD;
        }
        if (modifier != 0)
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
            HPText.text = hp.ToString() + '/' + maxHP.ToString();
            healthBar.value = (float)hp / maxHP;
        }
    }

    void UpdateShieldStatus(int modifier)
    {
        if (modifier < 0)
        {
            nextCharge = Time.time + shieldRegenCD;
        }
        if (modifier != 0)
        {
            shield += modifier;
            if (shield <= 0)
            {
                shield = 0;
            }
            if (shield > maxShield)
            {
                shield = maxShield;
            }
            shieldText.text = shield.ToString() + '/' + maxShield.ToString();
            shieldBar.value = (float)shield / maxShield;
        }
    }


    //moves the player to a designated respawn point
    public void Die()
    {
        if (healthBar.gameObject.activeInHierarchy)
        {
            Destroy(healthBar.gameObject);
        }
        masterController.FinalMessage();
        Destroy(masterController.gameObject);
        Destroy(this.gameObject);
    }

    public void Reset()
    {
        ClearEffects();
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero;
        canFire = false;
        autofire = false;
        inRound = false;
    }

    public void Prepare(float prepareTime)
    {
        canMove = false;
        canFire = false;
        transform.position = respawnPoint;
        rb.velocity = Vector3.zero;
        UpdateHealthStatus(roundHeal);
        UpdateShieldStatus(maxShield);
        activateTime = Time.time + prepareTime;
        ready = true;
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
        weaponController.bulletBag = GameObject.Find("BulletBag").transform;
    }

    public override void TakeDamage(int rawDamage, CharController attacker)
    {
        //Calculate Damage
        int shieldDamage = 0;
        float damage = attacker.damageModifier * rawDamage;
        if (damage * attacker.shieldBreakMultiplier >= shield)
        {
            shieldDamage = (int)Math.Ceiling(shield / attacker.shieldBreakMultiplier);
        }
        else
        {
            shieldDamage = rawDamage;
        }
        int fleshDamage = rawDamage - shieldDamage;
        fleshDamage = (int)(fleshDamage * attacker.fleshDamageMultiplier);
        UpdateShieldStatus(-1 * shieldDamage);
        UpdateHealthStatus(-1 * fleshDamage);

        //Apply effects
        ApplyEffects(attacker,Time.time);
    }

    void tick(float currentTime)
    {
        //effective before end time
        if (currentTime < d_fireEndTime)
        {
            if (d_fireDamage >= shield)
            {
                UpdateShieldStatus(-1 * shield);
                UpdateHealthStatus(-1 * (d_fireDamage - shield));
            }
            else
            {
                UpdateShieldStatus(-1 * d_fireDamage);
            }
        }

        if (currentTime < d_poisonEndTime)
        {
            UpdateHealthStatus(-1 * d_poisonDamage);
        }

        //effective after end time
        if (currentTime > d_slowEndTime)
        {
            d_slowAmount = 1f;
        }

        if (currentTime > d_shockEndTime)
        {
            canFire = true;
        }
    }

    public void IncreaseBloodLust()
    {
        if (a_canBloodLust)
        {
            d_bloodLustModifier += a_bloodLustModifier;
        }
    }
}
