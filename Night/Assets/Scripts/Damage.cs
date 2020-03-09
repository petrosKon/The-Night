﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Tags")]
    public string enemyTag = "Enemy";
    public string trapTag = "Trap";
    public string projectileTag = "Projectile";
    public string plantFlamethrowerTag = "PlantFlamethrower";
    public string chicken = "Chicken";



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(trapTag) || other.CompareTag(projectileTag) || other.CompareTag(chicken))
        {
            FindObjectOfType<GameManager>().DamagePlayer();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(plantFlamethrowerTag))
        {
            FindObjectOfType<GameManager>().KillPlayer();
        }
    }
}
