using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianCombat : MonoBehaviour
{
    [Header("Player")]
    public CharacterController playerCharacterController;
    public float timeBtwAttacks = 2f;

    [Header("Explosion Variables")]
    public GameObject explosionAttack;           //the explosion that is triggered
    public int maxNumberOfExplosions = 2;           //num of explosions that the player is able to trigger
    public int explosionCount = 0;                  //num of explosions that the player has triggered
    GameObject clone;                   //this variable is needed in order to see if the player attacks!!

    // Update is called once per frame
    void Update()
    {
        Attack();

        if(clone != null)
        {
            playerCharacterController.enabled = false;
        }
        else{

            playerCharacterController.enabled = true;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (explosionCount < maxNumberOfExplosions)
            {
                GetComponentInChildren<Animator>().SetTrigger("Cast Spell");
                
                Invoke("Spawn", 0.2f);
                explosionCount++;
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            GetComponentInChildren<Animator>().SetTrigger("Rapid Attack");
        }

    }

    void Spawn()
    {
        clone = Instantiate(explosionAttack, transform.position, Quaternion.identity);
        Destroy(clone, 0.3f);
    }
}
