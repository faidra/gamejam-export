using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
    public Player Player;

    public bool IsAutoEffect = true;
    public float AutoEffectIntervalSeconds = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Affect()
    {
    }
}
