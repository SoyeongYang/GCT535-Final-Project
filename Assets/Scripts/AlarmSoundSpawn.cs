using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSoundSpawn : MonoBehaviour
{
    public AK.Wwise.Event alarmEvent;
    private bool isAlarmPlaying = false;

    public void StartAlarm()
    {
        if (!isAlarmPlaying)
        {
            // Debug.Log("StartAlarm");
            alarmEvent.Post(gameObject);
            isAlarmPlaying = true;
        }
    }

    public void StopAlarm()
    {
        if (isAlarmPlaying)
        {
            // Debug.Log("StopAlarm");
            alarmEvent.Stop(gameObject);
            isAlarmPlaying = false;
        }
    }
}
