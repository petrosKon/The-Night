using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;

    void Start()
    {

        if (this.gameObject.tag == "EnemyEasy")
        {
            damageToGive = 20;
        }
        else if (this.gameObject.tag == "EnemyMedium")
        {
            damageToGive = 30;
        }
        else if (this.gameObject.tag == "EnemyHard")
        {
            damageToGive = 50;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);

            FindObjectOfType<GameManager>().ScaleLerper(0.8f, 1.2f);

            Destroy(gameObject);

        }
    }
}
