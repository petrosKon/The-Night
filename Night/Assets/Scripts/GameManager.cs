using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    public int currentPoints;
    public TextMeshProUGUI pointsText;
    public GameObject player;
    public Renderer playerRenderer;
    public GameObject particleDeathEffectPlayerPrefab;


    public float invincibilityLength = 0f;
    private float flashCounter;
    public float flashLength = 0.1f;

    [Header("Static Variables")]
    public static Vector3 maxPlayerScale = new Vector3(3f, 3f, 3f);
    public static Vector3 minPlayerScale = new Vector3(1.2f, 1.2f, 1.2f);
    public static float minPlayerSpeed = 5f;
    public static float maxPlayerSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityLength > 0f)
        {
            invincibilityLength -= Time.deltaTime;

            if (playerRenderer != null) {

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
        else
        {
            invincibilityLength = 0f;
        }
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
        if (invincibilityLength == 0f)
        {
            invincibilityLength = 3f;

            if (player.transform.localScale == minPlayerScale)
            {

                FindObjectOfType<PlayerController>().animator.SetTrigger("Die");

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

            }
        }
           
    }
}
