using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Player Variables")]
    public GameObject player; //our player
    public Renderer playerRenderer;  //this is needed for material change and flickering
    public GameObject particleDeathEffectPlayerPrefab;   //spawn particles case of enemy death
    public float invincibilityLength = 0f; //lenght of the invicibility in case of damage
    private float flashCounter;
    public float flashLength = 0.1f;

    [Header("Static Variables")]
    public static Vector3 maxPlayerScale = new Vector3(3f, 3f, 3f);
    public static Vector3 minPlayerScale = new Vector3(1.2f, 1.2f, 1.2f);
    public static float minPlayerSpeed = 6f;
    public static float maxPlayerSpeed = 15f;

    [Header("Health")]
    public int health;
    private int maxHealth = 2;

    public static float destroyParticleEffectTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //this means that the player falls off the edge!!
        if(player != null)
        {
            if (player.transform.position.y < -3f)
            {
                Instantiate(particleDeathEffectPlayerPrefab, player.transform.position, Quaternion.identity);

                Destroy(player);
            }
        }

        IFrames();

    }

    //Invicibility frames
    private void IFrames()
    {
        invincibilityLength -= Time.deltaTime;
        if (playerRenderer != null)
        {

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invincibilityLength <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    //kills the player case the player doesn't have a power up star with him
    public void DamagePlayer()
    {
        if (invincibilityLength < 0f)
        {
            invincibilityLength = 3f;

            health--;

            if(health <= 0)
            {
                KillPlayer();
            }
        }        
    }


    //destroys the player independant of power ups
    public void KillPlayer()
    {
        Destroy(player.gameObject);

        Instantiate(particleDeathEffectPlayerPrefab, player.transform.position, Quaternion.identity);
    }

    public void HealPlayer(int healAmount)
    {
        if(health < maxHealth)
        {
            health += healAmount;
        }
    }
}
