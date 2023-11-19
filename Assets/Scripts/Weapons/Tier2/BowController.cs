using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : WeaponController
{
    bool holding;
    GameObject holdingArrow;
    float holdingDamage;
    float maxMultiplier;
    float chargeRate;
    CharController player;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fireRate = 5f;
        bulletSpeed = 20f;
        damage = 5;
        holding = false;
        maxMultiplier = 3;
        chargeRate = (maxMultiplier-1);
    }

    void Start()
    {
        player = transform.parent.gameObject.GetComponent<CharController>();
    }

    void Update()
    {
        if (holding && holdingDamage < maxMultiplier * damage)
        {
            holdingArrow.transform.localScale += Vector3.one * chargeRate/5 * Time.deltaTime;
            holdingDamage += chargeRate * damage * Time.deltaTime;
        }
    }

    public override void Fire(float angle, float fireRateModifier)
    {
        if (nextFire < Time.time && !holding)
        {
            player.selfSlow = 0.7f;
            holdingArrow = Instantiate(bullet, transform.position, bullet.transform.rotation, transform);
            holdingArrow.transform.rotation *= transform.rotation;
            holdingDamage = damage;
            holding = true;
            //audioSource.Play();
        }
    }

    public override void Release(float angle, float fireRateModifier) 
    {
        if (holding)
        {
            player.selfSlow = 1f;
            holdingArrow.transform.parent = bulletBag;
            holdingArrow.name = ((int)holdingDamage).ToString();
            holdingArrow.GetComponent<AttackerInfo>().attackerController = player;
            holdingArrow.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 90)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90)), 0f) * bulletSpeed;
            holding=false;
            nextFire = Time.time + 1f/(fireRate * fireRateModifier);
            //audioSource.Play();
        }
    }

    public override void AltFire(float angle, float fireRateModifier) { }

    public override void AltRelease(float angle, float fireRateModifier) { }
}
