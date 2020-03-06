using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    public int currentPoints;
    public TextMeshProUGUI pointsText; //our points display

    [Header("Player Variables")]
    public GameObject player; //our player
    public Renderer playerRenderer;  //this is needed for material change and flickering
    public Material playerStartingMaterial, playerPowerUpMaterial;  //change material case of player power up
    public GameObject particleDeathEffectPlayerPrefab;   //spawn particles case of enemy death
    public float invincibilityLength = 0f; //lenght of the invicibility in case of damage
    public float maxPowerLength = 5f;  //length of the power up
    private bool powerUp = false;  //determines if the player is powered up (reaches max scale) and prevents from flickering
    private float flashCounter;
    public float flashLength = 0.1f;

    [Header("Static Variables")]
    public static Vector3 maxPlayerScale = new Vector3(3f, 3f, 3f);
    public static Vector3 minPlayerScale = new Vector3(1.2f, 1.2f, 1.2f);
    public static float minPlayerSpeed = 6f;
    public static float maxPlayerSpeed = 15f;

    [Header("Health")]
    public int health;
    public int maxHealth = 2;

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

        /*
        //This block of code makes the player capable of destroying emenies
        if (IsPlayerMaxSize())
        {
            invincibilityLength -= Time.deltaTime;

            if (invincibilityLength <= 0)
            {
                //loses all his power
                powerUp = false;

                //loses the tag, need to lose because he will destroy again the enemies
                player.gameObject.tag = "Player";

                playerRenderer.material = playerStartingMaterial;

                //this variables calculates the player size and returns him to the starting height
                float playerSizeMultiplier = minPlayerScale.magnitude / player.transform.localScale.magnitude;
                float playerSpeedMultiplier = maxPlayerSpeed / FindObjectOfType<PlayerController>().moveSpeed;

                ScaleLerper(playerSizeMultiplier, playerSpeedMultiplier);
            }
        }
        //This block of code flickers the player when he takes damage
        else
        {
         IFrames();

        }*/
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

    //checks if the player is max size!!
    private bool IsPlayerMaxSize()
    {
        if(player != null)
        {
            if (player.transform.localScale == maxPlayerScale)
            {
                if (!powerUp)
                {
                    powerUp = true;

                    //if the player is called playerMax then he can destroy enemies
                    player.gameObject.tag = "PlayerMax";

                    //the invicibility lasts for 5 seconds
                    invincibilityLength = 5f;

                    playerRenderer.material = playerPowerUpMaterial;

                    FindObjectOfType<PlayerController>().moveSpeed = maxPlayerSpeed;

                }

                return true;

            }

        }

        return false;
    }

    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
        pointsText.text = "Points: " + currentPoints.ToString();

    }

    //increase the size of the player while increasing/decreasing his movement speed
    public void ScaleLerper(float sizeMultiplier, float movementSpeedMultiplier)
    {
        //TODO: Create a scaling up animation!!!!
         /* Vector3 minScale = player.transform.localScale;
          Vector3 maxScale = minScale * sizeIncreaseMultiplier;
          Debug.Log("Min Scale: " + minScale);
          Debug.Log("Max Scale: " + maxScale);

          float speed = 5f;
          float duration = 5f;
          float i = 0.0f;
          float rate = (1.0f / duration) * speed;
          Debug.Log("Rate: " + rate);

          while(i < 1.0f)
          {
              i += Time.deltaTime * rate;
              Debug.Log("Rate: " + rate);
              player.transform.localScale = Vector3.Lerp(minScale, maxScale, i);
              Debug.Log("Local Scale: " + transform.localScale);
          }*/

        if(player.transform.localScale.magnitude * sizeMultiplier < maxPlayerScale.magnitude)
        {
            player.transform.localScale *= sizeMultiplier;
            //Reduce the player speed
            FindObjectOfType<PlayerController>().moveSpeed *= movementSpeedMultiplier;

        }
        else
        {
            player.transform.localScale = maxPlayerScale;
            //Reduce the player speed
            FindObjectOfType<PlayerController>().moveSpeed = minPlayerSpeed;
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

            //uncomment to use size changer
            /*
            if (player.transform.localScale == minPlayerScale)
            {
                Instantiate(particleDeathEffectPlayerPrefab, player.transform.position, Quaternion.identity);

                Destroy(player.gameObject);

            }
            else
            {
                //this variables calculates the player size and returns him to the starting height
                float playerSizeMultiplier = minPlayerScale.magnitude / player.transform.localScale.magnitude;
                float playerSpeedMultiplier = maxPlayerSpeed / FindObjectOfType<PlayerController>().moveSpeed;

                ScaleLerper(playerSizeMultiplier, playerSpeedMultiplier);

                playerRenderer.enabled = false;

                flashCounter = flashLength;

            }*/
        }        
    }


    //destroys the player independant of power ups
    public void KillPlayer()
    {
        Destroy(player.gameObject);

        Instantiate(particleDeathEffectPlayerPrefab, player.transform.position, Quaternion.identity);
    }
}
