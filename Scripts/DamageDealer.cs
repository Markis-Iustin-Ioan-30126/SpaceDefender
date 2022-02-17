using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;
    [SerializeField] AudioClip damageDealerSound;

    public int GetDamege() { return damage; }

    public void DestroyDamageDealer()
    {
        Destroy(this.gameObject);
        //AudioSource.PlayClipAtPoint(damageDealerSound,Camera.main.transform.position,0.2f);
    }

    public void IncreaseDamage(int damage)
    {
        this.damage += damage;
    }
}
