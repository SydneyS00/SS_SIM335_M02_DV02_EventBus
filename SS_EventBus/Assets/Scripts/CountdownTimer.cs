using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private float _currentTime;
    private float duration = 3.0f;

    //OnEnable and OnDisable are the most significant lines in this class 
    //EveryTime the CountdownTimer object is enabled, the Subscribe() method
    //  is called. And when the opposite happens, it is disabled and unsubscribes.
    //This ensures that the object is listening to an event when its active or 
    //  doesn't get called by our RaceEventBus when its disabled or destroyed.

    //Remember that when the StartTimer() method of this class is called, it is 
    //  being called by the RaceEventBus every time the COUNTDOWN event is 
    //  published. 
    void OnEnable()
    {
        RaceEventBus.Subscribe(RaceEventType.COUNTDOWN, StartTimer);    
    }

    void OnDisable()
    {
        RaceEventBus.Unsubscribe(RaceEventType.COUNTDOWN, StartTimer);
    }

    private void StartTimer()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        _currentTime = duration;

        while(_currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _currentTime--;
        }

        RaceEventBus.Publish(RaceEventType.START);
    }

    void OnGUI()
    {
        GUI.color = Color.blue;
        GUI.Label(new Rect(125, 0, 100, 20), "COUNTDOWN: " + _currentTime);
    }
}
