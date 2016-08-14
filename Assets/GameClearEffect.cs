using UnityEngine;
using System.Collections;

public class GameClearEffect : Effect {
    public override void Affect()
    {
        Player.GameClear();
    }
}
