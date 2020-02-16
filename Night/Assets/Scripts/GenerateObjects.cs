using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject pointCrystal;
    public GameObject powerUpStar;
    public int xPos;
    public int zPos;
    public int enemyCount,pointCrystalCount,powerUpStarCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLevel());
    }

    IEnumerator SpawnLevel()
    {
        while (enemyCount < 5)
        {
            xPos = Random.Range(48, 5);
            zPos = Random.Range(-45, -4);
            Instantiate(theEnemy, new Vector3(xPos,0f,zPos),Quaternion.identity);
            enemyCount++;
        }

        while (pointCrystalCount < 50)
        {
            xPos = Random.Range(3, 48);
            zPos = Random.Range(-6, -49);
            Instantiate(pointCrystal, new Vector3(xPos, 1.3f, zPos), Quaternion.identity);
            pointCrystalCount++;
        }

        while (powerUpStarCount < 3)
        {
            xPos = Random.Range(3, 48);
            zPos = Random.Range(-6, -49);
            Instantiate(powerUpStar, new Vector3(xPos, 1.4f, zPos), Quaternion.identity);
            powerUpStarCount++;
        }
        yield return new WaitForSeconds(0f);

    }
}
