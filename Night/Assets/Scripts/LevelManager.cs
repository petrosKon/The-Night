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

    private float levelLength = 50f; //we start at 0 and we quickly generate our map
    float levelWidthLength = 0f;
    public int mapWidth; //map width, Random
    public int mapHeight; //map height, Random

     void Awake()
     {
          //random map height
         mapHeight = Random.Range(5, 10);
     }

    // Start is called before the first frame update
    void Start(){


        for (int i = 0; i < mapHeight; i++)
        {
            GenerateRandomLevel(levelLength, levelWidthLength);
            mapWidth = Random.Range(5, 10);
            int randomTile = Random.Range(-50, 50);

            for (int j = 0; j < mapWidth; j++)
            {
                levelWidthLength += Mathf.Sign(randomTile) * 50f;
                GenerateRandomLevel(levelLength, levelWidthLength);
                Debug.Log("Before: " + levelWidthLength);
            }

            levelWidthLength -= Mathf.Sign(randomTile) * 50f;
            levelLength += 50f;
            Debug.Log("After: " + levelWidthLength);

        }

        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRandomLevel(float levelLength, float levelWidthLength)
    {
        //turret level is always the last level      
        int randomLevel = Random.Range(0, levels.Length);
        Instantiate(levels[randomLevel], new Vector3(levelLength, 0f, levelWidthLength), Quaternion.identity);

    }
}
