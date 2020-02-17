using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeFlowerScale : MonoBehaviour
{
    private GameObject[] flowers;

    // Start is called before the first frame update
    void Start()
    {
        if(flowers == null)
        {
            flowers = GameObject.FindGameObjectsWithTag("Flower");
            Debug.Log(flowers.Length);

            for (int i = 0; i < flowers.Length; i++)
            {
                float randomSize = Random.Range(0.2f, 0.6f);
                Vector3 randomScale = new Vector3(randomSize, randomSize, randomSize);
                flowers[i].transform.localScale = randomScale;
            }
        }     
    }

}
