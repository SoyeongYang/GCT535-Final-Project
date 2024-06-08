using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSoundSpawn : MonoBehaviour
{
    public AK.Wwise.Event alarmEvent;
    public AK.Wwise.Event alarmOff;
    private bool isAlarmPlaying = false;
    private bool isAlarmOff = false;

    public void StartAlarm()
    {
        if (!isAlarmPlaying)
        {
            // Debug.Log("StartAlarm");
            alarmEvent.Post(gameObject);
            isAlarmPlaying = true;
            isAlarmOff = true;
        }
    }

    public void StopAlarm()
    {
        if (isAlarmPlaying)
        {
            // Debug.Log("StopAlarm");
            if (isAlarmOff) alarmOff.Post(gameObject);
            alarmEvent.Stop(gameObject);
            isAlarmPlaying = false;
        }
        isAlarmOff = false;
    }
}
