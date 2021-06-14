using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{

    [SerializeField] float currentScore = 0f;
    [SerializeField] [Range(0,100)] float gameSpeed = 1f;

    int currentLevel = 0;
    float resetScore = 0;
    float deleteScore = 0;

    [Header("Player Stats")]
    float health = 0;
    int damage = 0;
    float movementSpeed = 0;
    float attackSpeed = 0;
    bool resetStats = false;

    private void Awake()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        deleteScore = resetScore;
        resetScore = 0;
        if(resetStats == true)
        {
            FindObjectOfType<Player>().UpgradePlayer(health, damage, movementSpeed, attackSpeed);
            resetStats = false;
        }
    }

    private void Update()
    {
        SetGameSpeed();
    }

    public void AddScore(float EnemyScoreValue)
    {
        currentScore += EnemyScoreValue;
        resetScore += EnemyScoreValue;
    }

    public float GetScore()
    {
        return currentScore;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void SetGameSpeed()
    {
        Time.timeScale = gameSpeed;
    }

    public void SetCurrentLevel(int i)
    {
        currentLevel = i;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void RestartLevelScore()
    {
        currentScore = currentScore - deleteScore;
    }

    public void GetStats(float health, int damage, float movementSpeed, float attackSpeed)
    {
        this.health = health;
        this.damage = damage;
        this.movementSpeed = movementSpeed;
        this.attackSpeed = attackSpeed;
    }

    public void RestartStats()
    {
        FindObjectOfType<Player>().UpgradePlayer(health, damage, movementSpeed, attackSpeed);
    }

    public void SetBool()
    {
        resetStats = true;
    }
}
