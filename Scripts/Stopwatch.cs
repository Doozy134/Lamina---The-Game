using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = false;
    float currentTime;
    private TimeSpan time;
    private double timeTaken;

    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if (stopwatchActive == true)
        {
            //add time since last frame
            currentTime += Time.deltaTime;
        }
        //convert into timespan
        time = TimeSpan.FromSeconds(currentTime);
    }

    public void startStopwatch()
    {
        //start stopwatch
        stopwatchActive = true;
    }

    public double stopStopwatch()
    {
        //stopping adding time
        stopwatchActive = false;
        //getting seconds
        timeTaken = time.TotalSeconds;
        //resetting clock
        currentTime = 0;

        return timeTaken;
    }
}
