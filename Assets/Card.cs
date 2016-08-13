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
    SpriteRenderer Front;
    [SerializeField]
    SpriteRenderer Back;

    [SerializeField]
    int Place;

    public BigInteger ThisScore { get { return new BigInteger(1) << Place; } }
    public bool IsOn { get { return (Player.Score >> Place & 1) > 0; } }

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
        Player.AddScore(ThisScore);
    }
}
