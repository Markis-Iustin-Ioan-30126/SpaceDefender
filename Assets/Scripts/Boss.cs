using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
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
    [SerializeField] [Range(0, 1)] float explosionSoundVolume;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume;

    //cache refrences
    GameStatus gameStatus;

    private void Start()
    {
        shootCounter = Random.Range(minShootTime, maxShootTime);
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void Update()
    {
        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0)
            Fire();
    }

    private void Fire()
    {
        GameObject ammo;
        ammo = Instantiate(this.projectile, this.transform.position, Quaternion.identity);
        if (gameObject.tag != "RandomBullet")
            ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        else
            ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-4, 4), -projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
        shootCounter = Random.Range(minShootTime, maxShootTime);
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
