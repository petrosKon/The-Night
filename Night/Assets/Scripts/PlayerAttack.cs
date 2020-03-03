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
    public float maxFlamethrowerSeconds = -2f;           //num of explosions that the player is able to trigger
    public float currentFlamethrowerSeconds = 0f;       //num of explosions that the player has triggered

    [Header("Boximon")]
    [SerializeField]
    private GameObject boximonGameObject;

    // Start is called before the first frame update
    void Start()
    {
        currentFlamethrowerSeconds = maxFlamethrowerSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        currentFlamethrowerSeconds -= Time.deltaTime;
        if(currentFlamethrowerSeconds <= 0f)
        {
            flamethrower.SetActive(false);

        }

    }

    private void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {     
            //animator.SetTrigger("Attack 01");
            if (Mathf.Abs(currentFlamethrowerSeconds) >= maxFlamethrowerSeconds)
            {
                flamethrower.SetActive(true);
                currentFlamethrowerSeconds = 2f;
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
