using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirePlant : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float distanceToMove;
    public float distanceToAttack;

    [Header("GameObjects")]
    public GameObject fireAttack;
    public GameObject petFirePlant;

    private Transform target;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {
            //if the player is not within our reach then move to the target position
            if (Vector3.Distance(transform.position, target.transform.position) < distanceToMove && target.position.y <= 2f)
            {
                if (!PlayerSeed())
                {
                    //if the player is within plant attack distance then attack
                    if (Vector3.Distance(transform.position, target.transform.position) < distanceToAttack)
                    {
                        LookRotation();
                        //lock the plant position
                        transform.position = this.transform.position;
                        StartCoroutine(Flamethrower());

                    }
                    //move towards the player
                    else
                    {
                        LookRotation();
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                        fireAttack.SetActive(false);
                        GetComponent<Animator>().SetBool("Walk", true);
                        GetComponent<Animator>().SetBool("Breath Attack", false);

                    }


                }
                else
                {
                    Instantiate(petFirePlant, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

            }
            //stand if player is out of reach
            else if ((Vector3.Distance(transform.position, target.transform.position) > distanceToMove))
            {
                GetComponent<Animator>().SetBool("Walk", false);
                GetComponent<Animator>().SetBool("Breath Attack", false);

            }
        }


    }

    private bool PlayerSeed()
    {
        try
        {
            foreach (GameObject item in inventory.slots)
            {
                if (item.transform.childCount > 0)
                {
                    //this means that the player holds a seed
                    if (item.transform.GetChild(0).CompareTag("Seed"))
                    {
                        Destroy(item.transform.GetChild(0).gameObject);
                        return true;
                    }
                }
            }

            return false;
        }
        //this means that there are no items in the inventory
        catch (NullReferenceException e)
        {
            return false;

        }
        return false;
    }

    void LookRotation()
    {
        Vector3 relativePos = target.position - transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }

    //due to the animation delay
    public IEnumerator Flamethrower()
    {
        GetComponent<Animator>().SetBool("Walk", false);

        GetComponent<Animator>().SetBool("Breath Attack", true);

        yield return new WaitForSeconds(1);

        fireAttack.SetActive(true);

    }
}
