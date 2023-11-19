using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : AttackerInfo
{
    public float fuse;

    private AudioSource audioSource;
    private float explodeTime;
    private bool exploded;
    private CircleCollider2D cc;
    private HashSet<GameObject> affected;
    private Transform bulletBag;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        explodeTime = Time.time + fuse;
        exploded = false;
        cc = GetComponent<CircleCollider2D>();
        affected = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && exploded)
        {
            GameObject grenade = transform.parent.gameObject;
            foreach (GameObject c in affected)
            {
                c.GetComponent<CharController>().TakeDamage(int.Parse(grenade.name), attackerController);
            }
            Destroy(transform.parent.gameObject);
        }
        if (Time.time > explodeTime && !exploded)
        {
            audioSource.Play();
            exploded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 && Time.time < explodeTime)
        {
            affected.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 && Time.time < explodeTime)
        {
            affected.Remove(other.gameObject);
        }
    }
}
