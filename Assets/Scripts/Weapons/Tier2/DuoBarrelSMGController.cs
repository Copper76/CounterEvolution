using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoBarrelSMGController : WeaponController
{
    /// Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fireRate = 10f;
        bulletSpeed = 20f;
        damage = 1;
    }

    public override void Fire(float angle, float fireRateModifier)
    {
        if (nextFire < Time.time)
        {
            for (int i = -1; i < 2; i+=2)
            {
                GameObject b = Instantiate(bullet, transform.position + new Vector3(0.2f * Mathf.Cos(Mathf.Deg2Rad * angle * i), 0.2f * Mathf.Sin(Mathf.Deg2Rad * angle * i), 0f), transform.rotation, bulletBag);
                b.name = damage.ToString();
                b.GetComponent<AttackerInfo>().attackerController = transform.parent.gameObject.GetComponent<CharController>();
                b.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 90)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90)), 0f) * bulletSpeed;
            }
            nextFire = Time.time + 1f/(fireRate * fireRateModifier);
            audioSource.Play();
        }
    }

    public override void Release(float angle, float fireRateModifier) { }

    public override void AltFire(float angle, float fireRateModifier) { }

    public override void AltRelease(float angle, float fireRateModifier) { }
}
