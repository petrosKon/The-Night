using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Explosion Variables")]
    public GameObject explosionRedPrefab;           //the explosion that is triggered
    public int maxNumberOfExplosions = 2;           //num of explosions that the player is able to trigger
    public int explosionCount = 0;                  //num of explosions that the player has triggered

    [Header("FireBreathing Variables")]
    public GameObject flamethrower;                //the explosion that is triggered
    public float flamethrowerActiveTime = 1f;           //num of explosions that the player is able to trigger
    public float flamethrowerCooldown = 0f;       //num of explosions that the player has triggered
    public float maxCooldownSeconds = 5f;

    [Header("Boximon")]
    [SerializeField]
    private GameObject boximonGameObject;


    // Update is called once per frame
    void Update()
    {
        Attack();

        if(flamethrowerCooldown >= 0f)
        {
            flamethrowerCooldown -= Time.deltaTime;

            //Calculate one second after the fire breathing is fired
            if(flamethrowerCooldown < maxCooldownSeconds - flamethrowerActiveTime)
            {
                flamethrower.SetActive(false);
            }
        }

    }

    private void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {     
            if(flamethrowerCooldown <= 0f)
            {
                flamethrower.SetActive(true);
                flamethrowerCooldown = maxCooldownSeconds;
            }
        
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // animator.SetTrigger("Attack 02");
            if (explosionCount < maxNumberOfExplosions)
            {
                GameObject clone = Instantiate(explosionRedPrefab, boximonGameObject.transform.position, Quaternion.identity);
                Destroy(clone, 0.3f);
                explosionCount++;
            }
        }
    }
}
