using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenChest : MonoBehaviour
{
    [Header("Particle Effect")]
    public GameObject pickupEffect;

    public float flamethrowerUpgradeMultiplier = 1.2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //give player another explosion
            FindObjectOfType<PlayerAttack>().maxNumberOfExplosions++;
            //reset the explosions
            FindObjectOfType<PlayerAttack>().explosionCount = 0;
            //upgrade the time of fire breath
            FindObjectOfType<PlayerAttack>().flamethrowerActiveTime *= flamethrowerUpgradeMultiplier;
            //Heal the player
            FindObjectOfType<GameManager>().health = FindObjectOfType<GameManager>().maxHealth;

            GameObject clone = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(clone, 0.2f);
        }
    }
}
