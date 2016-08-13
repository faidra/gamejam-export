using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    Timer Timer;
    [SerializeField]
    Player Player;

    public List<Effect> Effects = new List<Effect>();

    float lastUpdatedAt = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var now = Timer.Elapsed;
        if (Timer.Started)
        {
            foreach (var ef in Effects.Where(e => e.IsAutoEffect))
            {
                var times = (int)(now / ef.AutoEffectIntervalSeconds) - (int)(lastUpdatedAt / ef.AutoEffectIntervalSeconds);
                for (var i = 0; i < times; ++i)
                {
                    ef.Affect();
                }
            }
        }
        lastUpdatedAt = now;
    }

    public void AddEffect(Effect effect)
    {
        Effects.Add(effect);
        effect.transform.SetParent(transform);
        effect.transform.localPosition = new Vector3(-2 * Effects.Count, -8, 0);
        effect.Player = Player;

        if (!effect.IsAutoEffect) effect.Affect();
    }
}
