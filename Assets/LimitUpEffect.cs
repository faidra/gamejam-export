using UnityEngine;
using System.Collections;

public class LimitUpEffect : Effect
{
    [SerializeField]
    public int Num = 1;

    public override void Affect()
    {
        Player.ExpandLimit(Num);
    }
}
