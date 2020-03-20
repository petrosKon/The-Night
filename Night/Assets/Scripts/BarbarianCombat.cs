using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianCombat : MonoBehaviour
{
    [Header("Player")]
    public CharacterController playerCharacterController;

    [Header("Explosion Variables")]
    public GameObject explosionAttack;           //the explosion that is triggered
    public int maxNumberOfExplosions = 2;           //num of explosions that the player is able to trigger
    public int explosionCount = 0;                  //num of explosions that the player has triggered

    [Header("Claw Attack")]
    public GameObject clawAttack;
    public float maxTimeBtwAttacks = 2f;
    private float timeBtwAttacks;

    [Header("Enemies")]
    public Collider[] enemiesCollider;
    public float attackRange;

    GameObject clone;                   //this variable is needed in order to see if the player attacks!!

    private void Start()
    {
        timeBtwAttacks = maxTimeBtwAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (timeBtwAttacks > 0f)
        { 

            if (timeBtwAttacks < 0.5f)
            {
                clawAttack.SetActive(false);
                playerCharacterController.enabled = true;
            }
            else
            {
                playerCharacterController.enabled = false;

            }
            timeBtwAttacks -= Time.deltaTime;
        }
        else
        {
            timeBtwAttacks = 0f;
        }

        if (clone != null)
        {
            playerCharacterController.enabled = false;
        }
        else
        {
            playerCharacterController.enabled = true;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (explosionCount < maxNumberOfExplosions)
            {
                Invoke("ExplosionAttack", 0.2f);
                explosionCount++;
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (!clawAttack.activeSelf)
            {
                Invoke("BaseAttack", 0.2f);
            }
        }
    }

    void ExplosionAttack()
    {
        GetComponentInChildren<Animator>().SetTrigger("Cast Spell");
        clone = Instantiate(explosionAttack, transform.position, Quaternion.identity);
        Destroy(clone, 0.3f);
    }

    void BaseAttack()
    {
        GetComponentInChildren<Animator>().SetTrigger("Left Claw Attack");

        //we get the nearby colliders and if there is none around we spawn our object!!!
        enemiesCollider = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider col in enemiesCollider)
        {
            if (col.CompareTag("Enemy"))
            {
                Debug.Log("Attack");
                Destroy(col.gameObject);
            }
        }

        clawAttack.SetActive(true);
        timeBtwAttacks = maxTimeBtwAttacks;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
