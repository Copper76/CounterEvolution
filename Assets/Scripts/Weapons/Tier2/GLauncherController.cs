using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLauncherController : WeaponController
{
    /// Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        fireRate = 0.5f;
        bulletSpeed = 7f;
        damage = 15;
    }

    public override void Fire(float angle, float fireRateModifier)
    {
        if (nextFire < Time.time)
        {
            GameObject b = Instantiate(bullet, transform.position, bullet.transform.rotation, bulletBag);
            b.name = damage.ToString();
            b.transform.GetChild(0).GetComponent<AttackerInfo>().attackerController = transform.parent.gameObject.GetComponent<CharController>();
            b.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 90)), Mathf.Sin(Mathf.Deg2Rad * (angle + 90)), 0f) * bulletSpeed;
            nextFire = Time.time + 1f/(fireRate * fireRateModifier);
            audioSource.Play();
        }
    }

    public override void Release(float angle, float fireRateModifier) { }

    public override void AltFire(float angle, float fireRateModifier) { }

    public override void AltRelease(float angle, float fireRateModifier) { }
}
