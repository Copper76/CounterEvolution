using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public Transform bulletBag;
    public int damage;
    public float range;
    public bool melee;
    public bool canAutofire = false;

    [SerializeField] protected GameObject bullet;

    protected float fireRate;
    protected float altFireRate;
    protected float nextFire;
    protected float nextAltFire;
    protected float bulletSpeed;
    protected AudioSource audioSource;

    public abstract void Fire(float angle, float fireRateModifier);

    public abstract void Release(float angle, float fireRateModifier);

    public abstract void AltFire(float angle, float fireRateModifier);

    public abstract void AltRelease(float angle, float fireRateModifier);

}
