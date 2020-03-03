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
            Destroy(clone,1.0f);
            
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("ExplosionParticles"))
        {
            Destroy(gameObject);
            GameObject clone = Instantiate(particleDeathEffectEnemyPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(clone, 1.0f);
        }
    }
}
