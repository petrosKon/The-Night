using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    [Header("Variables")]
    public float speed;                 //speed of the archer
    public float stoppingDistance;      //The distance where the archer stops
    public float retreatDistance;       //The distance where the archer goes back
    public float distanceToShoot;       //the distance where the archer start shooting
    public float startTimeBtwShots;     //time between shots

    private float timeBtwShots;
    private float timeOfDay;             //Time of day accessed on lighting manager
    private bool nightPowerUp = false;           //Determine if the archer has powered up

    [Header("Objects")]
    public GameObject projectile;                   //Arrow projectile
    private Transform player;                       //Our Player
    private LightingManager lightingManager;        //In order to access the time of day

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        timeBtwShots = startTimeBtwShots;
        lightingManager = FindObjectOfType<LightingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ArcherMovement();

        NightPowerUp();
       
    }

    private void NightPowerUp()
    {

        float currentTime = lightingManager.timeOfDay;
        //Night Enters
        if (currentTime <= 6 || currentTime >= 19)
        {
            if (!nightPowerUp)
            {
                nightPowerUp = true;
                timeBtwShots = timeBtwShots / 2f;
                Debug.Log(timeBtwShots);
            }
        }
        else
        {
            if (nightPowerUp)
            {
                nightPowerUp = false;
                timeBtwShots *= 2f;
                Debug.Log(timeBtwShots);

            }
        }
    }

    private void ArcherMovement()
    {
        if (player != null)
        {
            //if player goes within radius then shoot the player
            if (Vector3.Distance(transform.position, player.position) < distanceToShoot)
            {
                if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
                {

                    transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    LookRotation();
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
                    LookRotation();
                    GetComponent<Animator>().SetBool("Run", true);

                }

                if (timeBtwShots <= 0)
                {
                    LookRotation();
                    GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
                    //rotate the arrow to match the firing point of our player!
                    clone.transform.Rotate(new Vector3(0f, -90f, 90f));
                    GetComponent<Animator>().SetBool("Arrow Attack", true);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
            //case the player runs away from the enemy radius
            //Stop the enemy
            else
            {
                GetComponent<Animator>().SetBool("Run", false);
                transform.position = this.transform.position;
            }
        }
    }

    void LookRotation()
    {
        Vector3 relativePos = player.position - transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
