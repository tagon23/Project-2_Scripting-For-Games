using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float currentHealth = 20f;
    [SerializeField] GameObject enemy;
    //enemy fires
    [SerializeField] ParticleSystem enemyDeath = null;

    public void Update()
    {
        if(currentHealth == 0)
        {
            enemyDeath.Play();
            currentHealth = -5;
        }
        if(currentHealth == -5)
        {
            enemy.GetComponent<Renderer>().enabled = false;
            enemy.GetComponent<Collider>().enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    
}
