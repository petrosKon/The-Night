using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;

    private float levelLength = 0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject level in levels)
        {
            levelLength += 50f;
            Instantiate(level, new Vector3(levelLength, 0f, 0f), Quaternion.identity);
            if(levelLength >= 50f)
            {
                Instantiate(level, new Vector3(levelLength, 0f, 50f), Quaternion.identity);
                Instantiate(level, new Vector3(levelLength, 0f, -50f), Quaternion.identity);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
