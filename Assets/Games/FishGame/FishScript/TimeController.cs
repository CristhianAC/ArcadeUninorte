using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public int min, sec;
    [SerializeField] TextMeshProUGUI timeText;
    public PlayerController playerController;
    public float timeLeft;
   
    
    private void Awake()
    {
        timeLeft = (min * 60) + sec;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        int tempMin = Mathf.FloorToInt(timeLeft / 60);
        int tempSec = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = string.Format("{0:00}:{1:00}", tempMin, tempSec);
        if (timeLeft <= 0)
            {
                
                playerController.Die();
            timeText.text = "00:00";
            Time.timeScale = 0;
            
            Destroy(this);
            
           
        }
            
    }
    
}
