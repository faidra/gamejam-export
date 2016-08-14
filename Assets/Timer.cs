using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text Text;

    float _startedAt;
    public bool Started;
    float _stoppedAt = 0f;

    public float Elapsed { get { return Started ? Time.time - _startedAt : _stoppedAt; } }

    public void StartsCount()
    {
        Started = true;
        _startedAt = Time.time;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var elapsed = Elapsed;
        var min = (int)(elapsed / 60);
        var sec = (int)(elapsed - 60 * min);
        var centSec = (int)((elapsed - 60 * min - sec) * 100);

        if (min > 0)
            Text.text = string.Format("{0}:{1:00}:{2:00}", min, sec, centSec);
        else
            Text.text = string.Format("{0}:{1:00}", sec, centSec);
    }

    internal void Stop()
    {
        _stoppedAt = Elapsed;
        Started = false;
    }
}
