using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateObjects : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemies;

    [Header("PickUp Items")]
    public GameObject pointCrystal;
    public GameObject powerUpStar;

    [Header("Teleporter")]
    public GameObject teleporter;

    [Header("Chests")]
    public GameObject woodenChest;

    [Header("Limits")]
    public Transform TopPoint, BottomPoint;

    [Header("Obstacles")]
    public Collider[] colliders;

    [Header("Variables")]
    public float yPositionSafety = 0.5f;
    public float obstacleCheckRadius = 1f;

    private int xPosTop,zPosTop;
    private int xPosBottom,zPosBottom;
    private int xPos, zPos;
    private int enemyCount,pointCrystalCount,powerUpStarCount,teleporterCount;
    private int randomNumberTeleporters;


    // Start is called before the first frame update
    void Start()
    {

        xPosTop = (int)TopPoint.position.x;
        zPosTop = (int)TopPoint.position.z;

        xPosBottom = (int)BottomPoint.position.x;
        zPosBottom = (int)BottomPoint.position.z;

        randomNumberTeleporters = Random.Range(-1, 2);

        StartCoroutine(SpawnLevel());
    }

    IEnumerator SpawnLevel()
    {
        int numOfRandomEnemies = Random.Range(0, 10);
        while (enemyCount < numOfRandomEnemies)
        {
            if (Random.Range(0, 10).Equals(9))
            {
                //the final enemy is the Fire Plant
                int enemyFirePlant = enemies.Length;
                PreventSpawnOverlap(enemies[Random.Range(0, enemyFirePlant)]);

            }
            else
            {
                PreventSpawnOverlap(enemies[Random.Range(0, enemies.Length - 1)]);

            }
            enemyCount++;
           
        }

        while (teleporterCount < randomNumberTeleporters)
        {
            PreventSpawnOverlap(teleporter);
            teleporterCount++;
        }

        int numOfRandomCrystals = Random.Range(0, 50);
        while (pointCrystalCount < numOfRandomCrystals)
        {
            PreventSpawnOverlap(pointCrystal);
            pointCrystalCount++;           
        }

        int numOfRandomStars = Random.Range(0, 6);
        while (powerUpStarCount < numOfRandomStars)
        {
            PreventSpawnOverlap(powerUpStar);
            powerUpStarCount++;
        }

        if (Random.Range(0, 10).Equals(9))
        {
            PreventSpawnOverlap(woodenChest);
        }
        
        yield return new WaitForSeconds(0.1f);

    }

    private void PreventSpawnOverlap(GameObject instatiatedObject)
    {
        bool validPosition = false;
        Vector3 randomPosition = new Vector3();
        RaycastHit hit;

        while (!validPosition)
        {
            validPosition = true;

            //random positions
            xPos = Random.Range(xPosTop, xPosBottom);
            zPos = Random.Range(zPosBottom, zPosTop);

            //The Y position is calculated when a raycast hit the layer, in order to determine it!!
            if(Physics.Raycast(new Vector3(xPos,9999f,zPos),Vector3.down, out hit, Mathf.Infinity))
            {
                randomPosition = new Vector3(xPos, hit.point.y + yPositionSafety, zPos);
            }

            //we get the nearby colliders and if there is none around we spawn our object!!!
            colliders = Physics.OverlapSphere(randomPosition, obstacleCheckRadius);

            //if our objects overlap with the colliders then reset the variable
            foreach (Collider col in colliders)
            {

                if (col.CompareTag("Obstacle") || col.CompareTag("Enemy"))
                {
                    validPosition = false;  
                }
            }
        }

        if(instatiatedObject == pointCrystal || instatiatedObject == powerUpStar)
        {
            //spawns the prefabs with a rotation especially the crystals and the stars!!
            Instantiate(instatiatedObject, randomPosition, Quaternion.Euler(270, 0, 0));
        }
        else{
            //spawns the prefabs with a rotation especially the crystals and the stars!!
            Instantiate(instatiatedObject, randomPosition, Quaternion.identity);
        }
    }
        
}
