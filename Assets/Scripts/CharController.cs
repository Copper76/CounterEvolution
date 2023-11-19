using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharController : MonoBehaviour
{
    //Common settings
    protected Vector3 weaponOffset = new Vector3(0.5f, 0f, -1f);
    protected float tickTime = 1f;

    //modifiers
    public float fireRateModifier;
    public float damageModifier;
    public float speed;
    public int maxHP;
    public float selfSlow = 1f;

    //Common Varaibles
    protected int hp;
    protected float angle;
    protected GameObject weapon;
    protected WeaponController weaponController;
    protected float nextTick;
    protected bool canFire;

    //Conditions
    //defender poison info
    protected float d_poisonEndTime = -1f;
    protected int d_poisonDamage;

    //defender fire info
    protected float d_fireEndTime = -1f;
    protected int d_fireDamage;

    //defender shock info
    protected float d_shockEndTime = -1f;

    //defender slow info
    protected float d_slowEndTime = -1f;
    protected float d_slowAmount = 1f;

    //temp attack bonus info
    public float d_bloodLustModifier = 0f;

    //Effect Modifiers
    //Shield and flesh damage Modifiers
    public float shieldBreakMultiplier = 1f;
    public float fleshDamageMultiplier = 1f;

    //deal dmaage straight to health
    public bool a_canPoison = false;
    public int a_poisonDamage = 1;
    public float a_poisonDuration = 3f;

    //deal damage to shield and health
    public bool a_canFire = false;
    public int a_fireDamage = 1;
    public float a_fireDuration = 3f;

    //stops the enemy from attacker
    public bool a_canShock = false;
    public float a_shockDuration = 0.1f;

    //slows enemy down
    public bool a_canSlow = false;
    public float a_slowAmount = 1.25f;
    public float a_slowDuration = 1f;

    //heal from damage dealt
    public bool a_canLifeSteal = false;
    public int a_lifeStealAmount = 1;

    //temp attack bonus lasting until the end of round
    public bool a_canBloodLust = false;
    public float a_bloodLustModifier = 0.05f;

    public abstract void EquipWeapon(GameObject newWeapon);

    public abstract void TakeDamage(int rawDamage,CharController attacker);

    public abstract void UpdateHealthStatus(int modifier);

    protected void ClearEffects()
    {
        d_poisonEndTime = -1f;
        d_fireEndTime = -1f;
        d_shockEndTime = -1f;
        d_slowEndTime = -1f;
        d_slowAmount = 1f;
        d_bloodLustModifier = 0f;
    }

    protected void ApplyEffects(CharController attacker,  float currentTime)
    {
        if (attacker.a_canPoison)
        {
            d_poisonEndTime = currentTime + attacker.a_poisonDuration;
            d_poisonDamage = attacker.a_poisonDamage;
        }

        if (attacker.a_canFire)
        {
            d_fireEndTime = currentTime + attacker.a_fireDuration;
            d_fireDamage = attacker.a_fireDamage;
        }

        if (attacker.a_canShock)
        {
            canFire = false;
            d_shockEndTime = currentTime + attacker.a_shockDuration;
        }

        if (attacker.a_canSlow)
        {
            d_slowEndTime = currentTime + attacker.a_slowDuration;
            d_slowAmount = attacker.a_slowAmount;
        }

        if (attacker.a_canLifeSteal)
        {
            attacker.UpdateHealthStatus(attacker.a_lifeStealAmount);
        }
    }
}
