using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualBarrelController : WeaponController
{
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fireRate = 1f;
        altFireRate = 0.66f;
        bulletSpeed = 10f;
        damage = 6;
    }

    public override void Fire(float angle, float fireRateModifier)
    {
        if (nextFire < Time.time)
        {
            for (int i = -1; i < 2; i++)
            {
                GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, i * 30f), bulletBag);
                b.name = damage.ToString();
                b.GetComponent<AttackerInfo>().attackerController = transform.parent.gameObject.GetComponent<CharController>();
                b.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 90 + 30 * i)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90 + 30 * i)), 0f) * bulletSpeed;
            }
            nextFire = Time.time + 1f/(fireRate * fireRateModifier);
            audioSource.Play();
        }
    }

    public override void Release(float angle, float fireRateModifier) { }

    public override void AltFire(float angle, float fireRateModifier)
    {
        if (nextFire < Time.time)
        {
            for (int i = -2; i < 3; i++)
            {
                GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, i * 15f), bulletBag);
                b.name = damage.ToString();
                b.GetComponent<AttackerInfo>().attackerController = transform.parent.gameObject.GetComponent<CharController>();
                b.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 90 + 15 * i)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90 + 15 * i)), 0f) * bulletSpeed;
            }
            nextFire = Time.time + 1f/(altFireRate * fireRateModifier);
            audioSource.Play();
        }
    }

    public override void AltRelease(float angle, float fireRateModifier) { }
}
