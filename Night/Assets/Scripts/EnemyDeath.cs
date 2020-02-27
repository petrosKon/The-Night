using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject particleDeathEffectEnemyPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" || other.tag == "PlayerMax")
        {
            
           GameObject clone = Instantiate(particleDeathEffectEnemyPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(clone,1.0f);
            
        }
    }
}
