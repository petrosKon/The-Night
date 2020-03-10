using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSeed : MonoBehaviour
{
    [Header("Item Thumbnail")]
    public GameObject seedItemThumbnail;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    //Item can be added to the inventory
                    inventory.isFull[i] = true;
                    Instantiate(seedItemThumbnail, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;

                }
            }
        }
    }
}
