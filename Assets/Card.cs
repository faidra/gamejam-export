using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using ScottGarland;

public class Card : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Player Player;
    [SerializeField]
    int Place;

    public BigInteger ThisScore { get { return new BigInteger(1) << Place; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.AddScore(ThisScore);
    }
}
