using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Player Stats")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float ylimitsCorrection; 
    [SerializeField] float xlimitsCorrection;
    [SerializeField] float health;
    [SerializeField] float currentHealth = 0;

    [Header("Player Effects")]
    [SerializeField] GameObject playerExplosionEffect;
    [SerializeField] GameObject playerHitEffect;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float explosionSoundVolume;
    [SerializeField] [Range(0, 1)] float laserSoundVolume;
    [SerializeField] GameObject damegeEffect;
    [SerializeField] GameObject damageState;

    [Header("Player projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] float laserTimeSpeed;

    Coroutine fireCoroutine;
    float xMin, xMax;
    float yMin, yMax;
    bool isDamaged = false;
    bool hasDamageState = false;

    GameObject activeDamageState;
    GameObject activeFlameEffect;

    private void Awake()
    {
        Singleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveLimits();
        currentHealth = health;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        if(isDamaged == false)
            DisplayDamageFlame();
        if (hasDamageState == false)
            DisplayDamageState();
    }

    private void Fire()
    {
        //GameObject laser;
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
            StopCoroutine(fireCoroutine);
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        { 
            GameObject laser;
            var laserPos = new Vector3(transform.position.x, transform.position.y+0.31f, transform.position.z);
            laser = Instantiate(laserPrefab, laserPos, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSound,Camera.main.transform.position,laserSoundVolume);
            yield return new WaitForSeconds(laserTimeSpeed);           
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltay = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newX = transform.position.x + deltaX;
        float newY = transform.position.y + deltay;
        transform.position = new Vector2(Mathf.Clamp(newX, xMin, xMax), Mathf.Clamp(newY, yMin, yMax));
    }

    private void SetUpMoveLimits()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xlimitsCorrection;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xlimitsCorrection;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + ylimitsCorrection;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - ylimitsCorrection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        currentHealth -= damageDealer.GetDamege();
        if (collision.gameObject.tag == "Bullet")
            Destroy(collision.gameObject);
        ManageLife();
    }

    private void ManageLife()
    {
        if (currentHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
            var explosion = Instantiate(playerExplosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            FindObjectOfType<Level>().LoadRetryScene();
            FindObjectOfType<GameStatus>().SetCurrentLevel(SceneManager.GetActiveScene().buildIndex);
            Destroy(this.gameObject);
        }
        else
        {
            var instance = Instantiate(playerHitEffect, transform.position, Quaternion.identity);
            Destroy(instance,1f);
        }
    }

    private float GetHealthPercentage()
    {
        float healthPercent;
        healthPercent = currentHealth / health;
        return healthPercent;
    }

    private void DisplayDamageFlame()
    {
        if(GetHealthPercentage() < 0.3)
        {
            activeFlameEffect = Instantiate(damegeEffect, new Vector3(transform.position.x+ UnityEngine.Random.Range(-0.2f, 0.2f),transform.position.y-0.1f,transform.position.z-0.2f), Quaternion.identity, gameObject.transform);
            isDamaged = true;
        }
    }

    private void DisplayDamageState()
    {
        if(GetHealthPercentage()<0.5)
        {
            var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            activeDamageState = Instantiate(damageState, pos, Quaternion.identity, gameObject.transform);
            hasDamageState = true;
        }
    }

    public float GetPlayerMaxHealth()
    {
        return health;
    }

    public float GetPlayerCurrentHealth()
    {
        return currentHealth;
    }

    public void UpgradePlayer(float health, int damage, float movementSpeed, float attackSpeed)
    {
        this.health += health;
        currentHealth += health;
        GetComponent<DamageDealer>().IncreaseDamage(damage);
        moveSpeed += movementSpeed;
        laserTimeSpeed = laserTimeSpeed - attackSpeed;
    }

    private void OnLevelWasLoaded(int level)
    {
        currentHealth = health;
        if (GetHealthPercentage() >= 0.5 && activeDamageState != null)
            Destroy(activeDamageState);
        if (GetHealthPercentage() >= 0.3 && activeFlameEffect != null)
            Destroy(activeFlameEffect);
    }

    private void Singleton()
    {
        if (FindObjectsOfType<Player>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }
}
