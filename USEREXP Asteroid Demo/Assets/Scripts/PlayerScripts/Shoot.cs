using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletBlastPrefab;
    public Transform muzzle;
    public float bulletSpeed;
    private float fireRate = 1.0f;
    public float fRMod = 3f; // fire Rate Modifier
    public bool canShoot = true;
    public bool isFiring = false;
    public int bulletBlastCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Fire());
        }
        if (ChainTracker.Instance != null && ChainTracker.Instance.currentChain % 10 == 0 && !isFiring)
        {
            StartCoroutine(BulletBlastRoutine());
        }
    }
    IEnumerator Fire()
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        yield return new WaitForSeconds(1 / fireRate / fRMod);
        canShoot = true;
    }

    IEnumerator BulletBlastRoutine()
    {
        isFiring = true;

        bulletBlast();

        yield return new WaitForSeconds(1 / fireRate / 5f);

        isFiring = false;
    }

    void bulletBlast()
    {
        if (Random.Range(1, 10) < 5)
        {
            for (int i = 0; i < bulletBlastCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletBlastCount;
                Vector3 bulletPosition = this.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 1.0f;

                GameObject bulletBlast = Instantiate(bulletBlastPrefab, muzzle.position, muzzle.rotation);
                bulletBlast.transform.up = (bulletPosition - this.transform.position).normalized;
                bulletBlast.GetComponent<Rigidbody2D>().velocity = bulletBlast.transform.up * bulletSpeed;
                Debug.Log("blast!!!!");
            }
        }      
    }
}