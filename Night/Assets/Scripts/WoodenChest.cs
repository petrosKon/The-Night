using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenChest : MonoBehaviour
{
    [Header("Particle Effect")]
    public GameObject pickupEffect;

    public float flamethrowerUpgradeMultiplier = 1.2f;
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //give player another explosion
            FindObjectOfType<BarbarianCombat>().maxNumberOfExplosions++;
            //reset the explosions
            FindObjectOfType<BarbarianCombat>().explosionCount = 0;
            //Heal the player by 1 point
            FindObjectOfType<HealthManager>().HealPlayer(healAmount);

            GameObject clone = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(clone, 0.2f);
        }
    }
}
