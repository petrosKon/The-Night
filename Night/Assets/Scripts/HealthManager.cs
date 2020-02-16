using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer(int damage) {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(PlayerManager.instance.gameObject);
        }

    }

    public void HealPlayer(int healAmount)
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else {
            currentHealth += healAmount;
        }
    }
}
