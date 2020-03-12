using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour
{
    [Header("Explosion Variables")]
    public GameObject explosionAttack;           //the explosion that is triggered
    public int maxNumberOfExplosions = 2;           //num of explosions that the player is able to trigger
    public int explosionCount = 0;                  //num of explosions that the player has triggered


    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // animator.SetTrigger("Attack 02");
            if (explosionCount < maxNumberOfExplosions)
            {
                GetComponentInChildren<Animator>().SetTrigger("Cast Spell");
                Invoke("Spawn", 0.2f);
                explosionCount++;
            }

        }

    }

    void Spawn()
    {
        GameObject clone = Instantiate(explosionAttack, transform.position, Quaternion.identity);
        clone.transform.Rotate(-90f, 0f, 0f);
        Destroy(clone, 0.3f);
    }
}
