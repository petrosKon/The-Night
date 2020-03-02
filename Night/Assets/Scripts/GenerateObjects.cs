using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateObjects : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject pointCrystal;
    public GameObject powerUpStar;
    public GameObject teleporter;
    public GameObject woodenChest;
    public Transform TopPoint, BottomPoint;
    private int xPosTop,zPosTop;
    private int xPosBottom,zPosBottom;
    private int xPos, zPos;
    private int enemyCount,pointCrystalCount,powerUpStarCount,teleporterCount;
    private int randomNumberTeleporters;

    //we determine where the power ups are going to spawn!!
    private readonly float pointCrystalYSpawnPosition = 0.3f;
    private readonly float enemyYSpawnPosition = 0f;
    private readonly float teleporterYSpawnPosition = 0f;
    private readonly float powerUpStarYSpawnPosition = 0.4f;

    private float obstacleCheckRadius = 1f;

    public Collider[] colliders;

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
        while (enemyCount < 5)
        {
           preventSpawnOverlap(theEnemy, enemyYSpawnPosition);
           enemyCount++;
           
        }

        while (teleporterCount < randomNumberTeleporters)
        {
            preventSpawnOverlap(teleporter,teleporterYSpawnPosition);
            teleporterCount++;
        }

        while (pointCrystalCount < 30)
        {
            preventSpawnOverlap(pointCrystal, pointCrystalYSpawnPosition);
            pointCrystalCount++;           
        }

        while (powerUpStarCount < 3)
        {
            preventSpawnOverlap(powerUpStar, powerUpStarYSpawnPosition);
            powerUpStarCount++;
        }

        if (Random.Range(0, 10).Equals(9))
        {
            preventSpawnOverlap(woodenChest, powerUpStarYSpawnPosition);
        }
        
        yield return new WaitForSeconds(0.1f);

    }

    private void preventSpawnOverlap(GameObject instatiatedObject,float spawnYPosition)
    {
        bool validPosition = false;
        Vector3 randomPosition = new Vector3();

        while (!validPosition)
        {
            validPosition = true;

            xPos = Random.Range(xPosTop, xPosBottom);
            zPos = Random.Range(zPosBottom, zPosTop);
            randomPosition = new Vector3(xPos, spawnYPosition, zPos);

            //we get the nearby colliders and if there is none around we spawn our object!!!
            colliders = Physics.OverlapSphere(randomPosition, obstacleCheckRadius);

            foreach (Collider col in colliders)
            {

                if (col.tag == "Obstacle" || col.tag == "Enemy")
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
