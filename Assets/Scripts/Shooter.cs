using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileLifetime = 2f;
    [SerializeField] float baseFireRate = 1.0f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0.2f;
    [SerializeField] float minFireRate = 0.1f;
    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    public float GetShootTimeForShip(float baseFireRate)
    {
        if (useAI)
        {
            float spawnTime = Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            return Mathf.Clamp(spawnTime, minFireRate, 2.0f);
        }
        else return baseFireRate;
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
            if (projectileBody != null)
            {
                //transform.up will points the green arrow direction on editor.
                projectileBody.velocity = transform.up * projectileSpeed;
            }

            Destroy(projectile, projectileLifetime);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(GetShootTimeForShip(baseFireRate));
        }

    }
}
