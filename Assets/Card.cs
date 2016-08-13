using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using ScottGarland;

public class Card : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    public Player Player;
    [SerializeField]
    SpriteRenderer Front;
    [SerializeField]
    SpriteRenderer Back;

    [SerializeField]
    public int Place;

    public BigInteger ThisScore { get { return new BigInteger(1) << Place; } }
    public bool IsOn { get { return (Player.Score >> Place & 1) > 0; } }
    public bool CanAddScore { get { return Player.CanAddScoreAt(Place); } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var isOn = IsOn;
        Front.gameObject.SetActive(isOn);
        Back.gameObject.SetActive(!isOn);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TryAddScore();
    }

    void TryAddScore()
    {
        if (CanAddScore) Player.AddScore(ThisScore);
    }
}
