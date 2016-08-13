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
        effect.transform.SetParent(transform);
        effect.transform.localPosition = GetPosition(Effects.Count);
        effect.Player = Player;
        Effects.Add(effect);

        if (!effect.IsAutoEffect) effect.Affect();
    }

    private Vector3 GetPosition(int index)
    {
        return new Vector3(-2 * (index % 5) + 10.5f, 6+((int)(index / 5)) * -2.7f, 0);
    }
}
