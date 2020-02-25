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
    public Transform TopPoint, BottomPoint;
    private int xPosTop,zPosTop;
    private int xPosBottom,zPosBottom;
    private int xPos, zPos;
    private int enemyCount,pointCrystalCount,powerUpStarCount,teleporterCount;
    private int randomNumberTeleporters;

    //we determine where the power ups are going to spawn!!
    private readonly float crystalYSpawnPosition = 1.3f;
    private readonly float powerUpStarYSpawnPosition = 1.4f;

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
           preventSpawnOverlap();
           Instantiate(theEnemy, new Vector3(xPos, 1f, zPos), Quaternion.identity);
           enemyCount++;
        }

        while (teleporterCount < randomNumberTeleporters)
        {
            preventSpawnOverlap();
            Instantiate(teleporter, new Vector3(xPos, 1f, zPos), Quaternion.identity);
            teleporterCount++;
        }

        while (pointCrystalCount < 30)
        {
            preventSpawnOverlap();
            Instantiate(pointCrystal, new Vector3(xPos, crystalYSpawnPosition, zPos), Quaternion.identity);
            pointCrystalCount++;           
        }

        while (powerUpStarCount < 3)
        {
            preventSpawnOverlap();
            Instantiate(powerUpStar, new Vector3(xPos, powerUpStarYSpawnPosition, zPos), Quaternion.identity);
            powerUpStarCount++;
        }
        
        yield return new WaitForSeconds(0f);

    }

    private void preventSpawnOverlap()
    {
        bool validPosition = false;

        while (!validPosition)
        {
            validPosition = true;

            xPos = Random.Range(xPosTop, xPosBottom);
            zPos = Random.Range(zPosBottom, zPosTop);
            Vector3 randomPosition = new Vector3(xPos, 0f, zPos);

            //we get the nearby colliders and if there is none around we spawn our object!!!
            colliders = Physics.OverlapSphere(randomPosition, obstacleCheckRadius);

            foreach (Collider col in colliders)
            {

                if (col.tag == "Obstacle")
                {
                    validPosition = false;  
                }
            }
        }
    } 
}
