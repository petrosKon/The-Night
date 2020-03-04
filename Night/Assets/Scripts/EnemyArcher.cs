using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float distanceToShoot;

    private float timeBtwShots;
    public float startTimeBtwShots;

    [Header("Objects")]
    public GameObject projectile;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        { 
            if(Vector3.Distance(transform.position, player.position) < distanceToShoot)
            {
                if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
                {

                    transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    LookWhereYouShoot();
                    this.GetComponent<Animator>().SetBool("Run", true);

                }
                else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
                {

                    transform.position = this.transform.position;
                    this.GetComponent<Animator>().SetBool("Run", false);


                }
                else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                    LookWhereYouShoot();
                    this.GetComponent<Animator>().SetBool("Run", true);

                }

                if (timeBtwShots <= 0)
                {
                    LookWhereYouShoot();
                    Instantiate(projectile, transform.position, transform.rotation);
                    this.GetComponent<Animator>().SetBool("Arrow Attack", true);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
           
        }
      
    }

    void LookWhereYouShoot()
    {
        Vector3 relativePos = player.position - transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
