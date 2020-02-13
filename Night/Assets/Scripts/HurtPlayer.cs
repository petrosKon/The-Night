using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;

    // Start is called before the first frame update
    void Start()
    {

        if (this.gameObject.tag == "EnemyEasy")
        {
            damageToGive = 1;
        }
        else if (this.gameObject.tag == "EnemyMedium")
        {
            damageToGive = 10;

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

        }
    }
}
