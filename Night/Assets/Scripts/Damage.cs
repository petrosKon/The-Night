using System.Collections;
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
    public string seed = "Seed";



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag)
            || other.CompareTag(trapTag)
            || other.CompareTag(projectileTag)
            || other.CompareTag(chicken) 
            || other.CompareTag(seed))
        {
            FindObjectOfType<HealthManager>().DamagePlayer();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(plantFlamethrowerTag))
        {
            FindObjectOfType<HealthManager>().KillPlayer();
        }
    }
}
