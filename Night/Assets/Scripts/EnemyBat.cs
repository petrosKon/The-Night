using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float distanceToAttack;

    [Header("GameObjects")]
    public GameObject soundwaveParticles;


    private float distanceToFollow = 5f;  //the player need to get really close to the bat
    private Transform playerTarget;
    private Vector3 offset;      //offset in which the plant must follow

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = PlayerManager.instance.player.transform;

        offset = new Vector3(0f, transform.position.y, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
        {
            if (Vector3.Distance(transform.position, playerTarget.position) <= distanceToFollow)
            {

                //create a very big distance in order for the player to not be able to escape the bat
                distanceToFollow = 100f;

                LookRotation(playerTarget);

                transform.position = Vector3.MoveTowards(transform.position, playerTarget.position + offset, speed * Time.deltaTime);

            }
        }

    }

    void LookRotation(Transform target)
    {
        Vector3 relativePos = target.position - transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }

}
