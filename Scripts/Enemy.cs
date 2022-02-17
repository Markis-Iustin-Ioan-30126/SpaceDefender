using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy atributes")]
    [SerializeField] float health = 200;
    [SerializeField] float EnemyScoreValue = 50f;
    [SerializeField] float shootCounter = 1f;

    [Header("Shooting atributes")]
    [SerializeField] float maxShootTime = 2f;
    [SerializeField] float minShootTime = 0.3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20;

    [Header("Effects")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0,1)] float explosionSoundVolume;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume;

    [Header("Boss")]
    [SerializeField] float bossMaxShootTime = 4f;
    [SerializeField] float bossMinShootTime = 2f;
    [SerializeField] GameObject bossProjectile;
    [SerializeField] float bossProjectileSpeed = 4;
    [SerializeField] AudioClip bossLaserSound;
    float bossShootCounter;
    //cache refrences
    GameStatus gameStatus;

    private void Start()
    {
        shootCounter = Random.Range(minShootTime, maxShootTime);
        bossShootCounter = UnityEngine.Random.Range(bossMinShootTime, bossMaxShootTime);
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void Update()
    {
        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0)
            Fire();
        bossShootCounter -= Time.deltaTime;
        if (bossShootCounter <= 0 && gameObject.tag == "Boss")
            BossFire();
    }

    private void Fire()
    {
        //Debug.Log(gameObject.tag);
        GameObject ammo;
        ammo = Instantiate(this.projectile, this.transform.position, Quaternion.identity);
        if(gameObject.tag == "RandomBullet")
            ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-4,4), -projectileSpeed);
        else
        {
            if(gameObject.tag == "Boss")
            {
                //Debug.Log("he");
                ammo.transform.position = new Vector2(transform.position.x - 3.623f, transform.position.y - 2.051f);
                ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-4, 4), -projectileSpeed);
                var ammo1 = Instantiate(this.projectile, new Vector2(transform.position.x+3.485f, transform.position.y - 2.051f), Quaternion.identity);
                ammo1.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-4, 4), -projectileSpeed);
            }
            else
            ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
        shootCounter = Random.Range(minShootTime, maxShootTime);
    }

    private void BossFire()
    {
        var bossLaser = Instantiate(bossProjectile, new Vector2(transform.position.x + 0.494f, transform.position.y - 1.461f), Quaternion.identity);
        var bossLaser1 = Instantiate(bossProjectile, new Vector2(transform.position.x - 0.501f, transform.position.y - 1.461f), Quaternion.identity);
        bossLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bossProjectileSpeed);
        bossLaser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bossProjectileSpeed);
        bossShootCounter = UnityEngine.Random.Range(bossMinShootTime, bossMaxShootTime);
        AudioSource.PlayClipAtPoint(bossLaserSound, Camera.main.transform.position, laserSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        health = health - damageDealer.GetDamege();
        //Destroy(collision.gameObject);
        if (collision.gameObject.tag == "Bullet")
            damageDealer.DestroyDamageDealer();
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //Debug.Log("aici");
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
            Destroy(exp, 2f);
            gameStatus.AddScore(EnemyScoreValue);
            FindObjectOfType<EnemySpawner>().EliminateEnemies();
        }
        else
        {
            GameObject exp1 = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(exp1, 0.5f);
        }
    }

    
}
