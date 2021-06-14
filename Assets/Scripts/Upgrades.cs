using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthPointsText;
    [SerializeField] TextMeshProUGUI damagePointsText;
    [SerializeField] TextMeshProUGUI attackSpeedPointsText;
    [SerializeField] TextMeshProUGUI movementSpeedPointsText;
    [SerializeField] TextMeshProUGUI availablePointsText;
    [SerializeField] int healthPoints = 0;
    [SerializeField] int damagePoints = 0;
    [SerializeField] int attackSpeedPoints = 0;
    [SerializeField] int movementSpeedPoints = 0;
    [SerializeField] int availablePoints = 5;

    [Header("Constant Upgrades Parameters")]
    [SerializeField] float health = 200;
    [SerializeField] int damage = 50;
    [SerializeField] float attackSpeed = 0.01f;
    [SerializeField] float movementSpeed = 1;

    private void Start()
    {
        availablePointsText.text = availablePoints.ToString();
    }

    public void UpgradeHealth()
    {   
        if(availablePoints > 0)
        {
            ManageAvailablePoints();
            healthPoints++;
            healthPointsText.text = healthPoints.ToString();
        }
    }

    public void UpgradeDamage()
    {
        if (availablePoints > 0)
        {
            ManageAvailablePoints();
            damagePoints++;
            damagePointsText.text = damagePoints.ToString();
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (availablePoints > 0)
        {
            ManageAvailablePoints();
            attackSpeedPoints++;
            attackSpeedPointsText.text = attackSpeedPoints.ToString();
        }
    }

    public void UpgradeMovementSpeed()
    {
        if (availablePoints > 0)
        {
            ManageAvailablePoints();
            movementSpeedPoints++;
            movementSpeedPointsText.text = movementSpeedPoints.ToString();
        }
    }

    public void ConfirmUpgrades()
    {
        var player = FindObjectOfType<Player>();
        player.UpgradePlayer(health*healthPoints, damage*damagePoints, movementSpeed*movementSpeedPoints, attackSpeed*attackSpeedPoints);
        FindObjectOfType<GameStatus>().GetStats(health * healthPoints, damage * damagePoints, movementSpeed * movementSpeedPoints, attackSpeed * attackSpeedPoints);
        //StartCoroutine(InstantiateNextSpawner());
        FindObjectOfType<EnemySpawner>().InstantiateNextSpawner();
        Destroy(gameObject);
    }

    private IEnumerator InstantiateNextSpawner()
    {
        Debug.Log("aici");
        yield return new WaitForSeconds(2f);
        Debug.Log("acolo");

        var ceva = FindObjectOfType<EnemySpawner>();
        Debug.Log(ceva.name);
    }

    private void ManageAvailablePoints()
    {
        availablePoints--;
        availablePointsText.text = availablePoints.ToString();
    }
}
