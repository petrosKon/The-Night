using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    public int currentPoints;
    public TextMeshProUGUI pointsText; //our points display

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
}
