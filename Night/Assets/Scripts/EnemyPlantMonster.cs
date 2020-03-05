using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlantMonster : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float distanceToMove;
    public float distanceToAttack;

    [Header("GameObjects")]
    public Transform target;
    public GameObject fireAttack;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            //if the player is within plant attack distance then attack
            if (Vector3.Distance(transform.position, target.transform.position) < distanceToAttack)
            {
                LookRotation();
                fireAttack.SetActive(true);
                transform.position = this.transform.position;
                GetComponent<Animator>().SetBool("Breath Attack", true);
                GetComponent<Animator>().SetBool("Walk", false);

            }
            //follow the player if he is within your reach and not on top of stairs
            else if (Vector3.Distance(transform.position, target.transform.position) < distanceToMove && target.position.y <= 2f)
            {
                LookRotation();
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                GetComponent<Animator>().SetBool("Walk", true);
                GetComponent<Animator>().SetBool("Breath Attack", false);


            }
            //stand if player is out of reach
            else
            {
                GetComponent<Animator>().SetBool("Walk", false);
                GetComponent<Animator>().SetBool("Breath Attack", false);

            }
        }
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
}
