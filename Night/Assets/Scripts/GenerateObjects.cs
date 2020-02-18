using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateObjects : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject pointCrystal;
    public GameObject powerUpStar;
    public Transform TopPoint, BottomPoint;
    private int xPosTop,zPosTop;
    private int xPosBottom,zPosBottom;
    private int xPos, zPos;
    private int enemyCount,pointCrystalCount,powerUpStarCount,flowerCount;

    //we determine where the power ups are going to spawn!!
    private readonly float crystalYSpawnPosition = 1.3f;
    private readonly float powerUpStarYSpawnPosition = 1.4f;

    // Start is called before the first frame update
    void Start()
    {

        xPosTop = (int)TopPoint.position.x;
        zPosTop = (int)TopPoint.position.z;    

        xPosBottom = (int)BottomPoint.position.x;
        zPosBottom = (int)BottomPoint.position.z;
        StartCoroutine(SpawnLevel());

    }

    IEnumerator SpawnLevel()
    {
        while (enemyCount < 5)
        {
            xPos = Random.Range(xPosTop, xPosBottom);
            zPos = Random.Range(zPosBottom, zPosTop);
            Instantiate(theEnemy, new Vector3(xPos,1f,zPos),Quaternion.identity);
            enemyCount++;
        }

        while (pointCrystalCount < 50)
        {
            xPos = Random.Range(xPosBottom, xPosTop);
            zPos = Random.Range(zPosBottom, zPosTop);
            Instantiate(pointCrystal, new Vector3(xPos, crystalYSpawnPosition, zPos), Quaternion.identity);
            pointCrystalCount++;
        }

        while (powerUpStarCount < 3)
        {
            xPos = Random.Range(xPosBottom, xPosTop);
            zPos = Random.Range(zPosBottom, zPosTop);
            Instantiate(powerUpStar, new Vector3(xPos, powerUpStarYSpawnPosition, zPos), Quaternion.identity);
            powerUpStarCount++;
        }

        yield return new WaitForSeconds(0f);

    }
}
