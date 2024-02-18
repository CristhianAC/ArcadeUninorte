using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public TimeController timeController;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addTime(int time)
    {
        timeController.timeLeft += time;
    }
    public void removeTime(int time)
    {
        timeController.timeLeft -= time;
    }
}
