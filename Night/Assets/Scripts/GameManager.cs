using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentPoints;
    public TextMeshProUGUI pointsText;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
        pointsText.text = "Points: " + currentPoints.ToString();

    }

    public void ScaleLerper(float sizeIncreaseMultiplier, float moveDecreaseMultiplier)
    {
        //TODO: Create a scaling up animation!!!!
          Vector3 minScale = player.transform.localScale;
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
          }
       // player.transform.localScale *= sizeIncreaseMultiplier;

        //Reduce the player speed
        FindObjectOfType<PlayerController>().moveSpeed *= moveDecreaseMultiplier;
        }

  

}
