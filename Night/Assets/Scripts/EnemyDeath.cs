using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [Header("Particles")]
    public GameObject particleDeathEffectEnemyPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet") || other.CompareTag("PlayerMax"))
        {
            
            Destroy(gameObject);
            GameObject clone = Instantiate(particleDeathEffectEnemyPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(clone, 0.2f);
            
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("ExplosionParticles") || other.CompareTag("PlantFlamethrower"))
        {
            Destroy(gameObject);
            GameObject clone = Instantiate(particleDeathEffectEnemyPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(clone, 0.2f);
        }
    }

    private void Update()
    {
        if(transform.position.y < -3f)
        {
            Destroy(gameObject);
            GameObject clone = Instantiate(particleDeathEffectEnemyPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(clone, 0.2f);
        }
    }
}
