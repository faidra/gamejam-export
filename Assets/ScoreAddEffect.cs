using UnityEngine;
using System.Collections;

public class ScoreAddEffect : Effect {
    [SerializeField]
    public int Place=0;

    public override void Affect()
    {
        Player.Add2Power(Place);
    }
}
