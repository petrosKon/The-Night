using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().DamagePlayer();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("PlantFlamethrower"))
        {
            FindObjectOfType<GameManager>().KillPlayer();
        }
    }
}
