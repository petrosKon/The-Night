using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject pointCrystal;
    public GameObject powerUpStar;
    public int xPosTop,zPosTop;
    public int xPosBottom,zPosBottom;
    public int xPos, zPos;
    public int enemyCount,pointCrystalCount,powerUpStarCount,flowerCount;

    // Start is called before the first frame update
    void Start()
    {
        xPosTop =  (int)GameObject.FindGameObjectWithTag("TopPoint").transform.position.x;
        zPosTop = (int) GameObject.FindGameObjectWithTag("TopPoint").transform.position.z;    

        xPosBottom = (int) GameObject.FindGameObjectWithTag("BottomPoint").transform.position.x;
        zPosBottom = (int)GameObject.FindGameObjectWithTag("BottomPoint").transform.position.z;
        StartCoroutine(SpawnLevel());

    }

    IEnumerator SpawnLevel()
    {
        while (enemyCount < 5)
        {
            xPos = Random.Range(xPosTop, xPosBottom);
            zPos = Random.Range(zPosBottom, zPosTop);
            Instantiate(theEnemy, new Vector3(xPos,0f,zPos),Quaternion.identity);
            enemyCount++;
        }

        while (pointCrystalCount < 50)
        {
            xPos = Random.Range(xPosBottom, xPosTop);
            zPos = Random.Range(zPosBottom, zPosTop);
            Instantiate(pointCrystal, new Vector3(xPos, 1.3f, zPos), Quaternion.identity);
            pointCrystalCount++;
        }

        while (powerUpStarCount < 3)
        {
            xPos = Random.Range(xPosBottom, xPosTop);
            zPos = Random.Range(zPosBottom, zPosTop);
            Instantiate(powerUpStar, new Vector3(xPos, 1.4f, zPos), Quaternion.identity);
            powerUpStarCount++;
        }

        yield return new WaitForSeconds(0f);

    }
}
