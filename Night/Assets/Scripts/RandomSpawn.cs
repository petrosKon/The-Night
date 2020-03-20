using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomSpawn : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemies;

    [Header("Traps")]
    public GameObject trap;

    [Header("Turret")]
    public GameObject turret;

    [Header("PickUp Items")]
    public GameObject crystal;
    public GameObject star;
    public GameObject seed;

    [Header("Teleporter")]
    public GameObject teleporter;

    [Header("Chests")]
    public GameObject woodenChest;

    [Header("Limits")]
    public Transform TopPoint, BottomPoint;

    [Header("Variables")]
    public float yPositionSafety = 0.5f;
    public float obstacleCheckRadius = 1f;

    [Header("Navigation Surface")]
    //nav mesh surface in order to build the nav mesh in real time!!!!
    public NavMeshSurface surface;

    [Header("Obstacles")]
    [HideInInspector]
    public Collider[] colliders;

    private int xPosTop, zPosTop;
    private int xPosBottom, zPosBottom;
    private int xPos, zPos;
    private int enemyCount, pointCrystalCount, powerUpStarCount, teleporterCount, trapCount, turretCount;
    private int randomNumberTeleporters;

    private void Awake()
    {
        surface.BuildNavMesh();
    }
    // Start is called before the first frame update
    void Start()
    {

        xPosTop = (int)TopPoint.position.x;
        zPosTop = (int)TopPoint.position.z;

        xPosBottom = (int)BottomPoint.position.x;
        zPosBottom = (int)BottomPoint.position.z;

        randomNumberTeleporters = Random.Range(-1, 5);

        StartCoroutine(SpawnLevel());
    }

    IEnumerator SpawnLevel()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        int numOfRandomEnemies = Random.Range(20, 50);
        //enemies
        while (enemyCount < numOfRandomEnemies)
        {
            PreventSpawnOverlap(enemies[Random.Range(0, enemies.Length)]);
            enemyCount++;

        }

        //teleporters
        while (teleporterCount < randomNumberTeleporters)
        {
            PreventSpawnOverlap(teleporter);
            teleporterCount++;
        }

        //point crystals : +5 points
        int numOfRandomCrystals = Random.Range(50, 150);
        while (pointCrystalCount < numOfRandomCrystals)
        {
            PreventSpawnOverlap(crystal);
            pointCrystalCount++;
        }

        //point stars : +100 points
        int numOfRandomStars = Random.Range(5, 10);
        while (powerUpStarCount < numOfRandomStars)
        {
            PreventSpawnOverlap(star);
            powerUpStarCount++;
        }

        int numOfTraps = Random.Range(20, 40);
        while (trapCount < numOfTraps)
        {
            PreventSpawnOverlap(trap);
            trapCount++;
        }

        int numOfTurrets = Random.Range(0,3);
        while (turretCount < numOfTurrets)
        {
            PreventSpawnOverlap(turret);
            turretCount++;
        }
        //if a fire plant is spawned then also spawn a seed

        if (Random.Range(0, 6).Equals(4))
        {
            PreventSpawnOverlap(seed);

        }

        //1 in 10 chance that a wooden chest will spawn
        if (Random.Range(0, 6).Equals(3))
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
            if (Physics.Raycast(new Vector3(xPos, 9999f, zPos), Vector3.down, out hit, Mathf.Infinity))
            {
                if (instatiatedObject == crystal || instatiatedObject == star || instatiatedObject == seed)
                {
                    randomPosition = new Vector3(xPos, hit.point.y + yPositionSafety, zPos);

                }
                else
                {
                    randomPosition = new Vector3(xPos, hit.point.y, zPos);
                }
            }

            //we get the nearby colliders and if there is none around we spawn our object!!!
            colliders = Physics.OverlapSphere(randomPosition, obstacleCheckRadius);

            //if our objects overlap with the colliders then reset the variable
            foreach (Collider col in colliders)
            {

                if (col.CompareTag("Enemy") || col.CompareTag("Trap"))
                {
                    validPosition = false;

                }
            }
        }

        if (instatiatedObject == crystal || instatiatedObject == star || instatiatedObject == seed)
        {
            //spawns the prefabs with a rotation especially the crystals and the stars!!
            Instantiate(instatiatedObject, randomPosition, Quaternion.Euler(270, 0, 0));
        }
        else
        {
            //spawns the prefabs with a rotation especially the crystals and the stars!!
            Instantiate(instatiatedObject, randomPosition, Quaternion.identity);
        }
    }

}
