using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(player, spawnPoint.transform.position, Quaternion.identity);
    }

  
}
