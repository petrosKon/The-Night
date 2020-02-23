using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    //premade levels
    public GameObject[] levels;

    //nav mesh surface in order to build the nav mesh in real time!!!!
    public NavMeshSurface surface;

    private float levelLength = 0f; //we start at 0 and we quickly generate our map
    float levelWidthLength = 0f;
    public int mapWidth; //map width, Random
    public int mapHeight; //map height, Random

     void Awake()
     {
          //random map height
         mapHeight = Random.Range(3, 10);
     }

    // Start is called before the first frame update
    void Start(){


        for (int i = 0; i < mapHeight; i++)
        {
            levelLength += 50f;
            mapWidth = Random.Range(3, 10);
            levelWidthLength = 0f;
            int randomTile = Random.Range(-50, 50);

            for (int j = 0; j < mapWidth; j++)
            {
                GenerateTurretLevel(levelLength, Mathf.Sign(randomTile) * levelWidthLength);
                levelWidthLength += 50f;
                Debug.Log("Before: " + levelWidthLength);
            }
            levelWidthLength = Mathf.Sign(randomTile) * 50f  * Random.Range(3, mapWidth) ;
            Debug.Log("After: " + levelWidthLength);

        }


        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTurretLevel(float levelLength,float levelWidthLength)
    {
        int turretLevel = levels.Length - 1;
        if (Random.Range(0,10) != 9)
        {
            int randomLevel = Random.Range(0, levels.Length - 1);
            Instantiate(levels[randomLevel], new Vector3(levelLength, 0f, levelWidthLength), Quaternion.identity);
        }
        else
        {
            Instantiate(levels[turretLevel], new Vector3(levelLength, 0f, levelWidthLength), Quaternion.identity);

        }
    }
}
