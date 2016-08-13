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
    public Effect Effect;
    [SerializeField]
    public int RemainingEffectsCount;

    [SerializeField]
    public int Place;

    public BigInteger ThisScore { get { return new BigInteger(1) << Place; } }
    public bool IsOn { get { return (Player.Score >> Place & 1) > 0; } }
    public bool CanAddScore { get { return Player.CanAddScoreAt(Place); } }
    public bool CanGetEffect { get { return !CanAddScore && IsOn && Effect != null && RemainingEffectsCount > 0; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsOn)
        {
            Back.gameObject.SetActive(true);
            Front.gameObject.SetActive(false);
            Effect.gameObject.SetActive(false);
        }
        else if (CanGetEffect)
        {
            Effect.gameObject.SetActive(true);
            Front.gameObject.SetActive(false);
            Back.gameObject.SetActive(false);
        }
        else
        {
            Front.gameObject.SetActive(true);
            Effect.gameObject.SetActive(false);
            Back.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TryAddScore();
        TryGetEffect();
    }

    void TryAddScore()
    {
        if (CanAddScore) Player.AddScore(ThisScore);
    }

    void TryGetEffect()
    {
        if (CanGetEffect)
        {
            Player.UseScore(ThisScore);
            if (RemainingEffectsCount == 1)
            {
                Player.AddEffect(Effect);
                Effect = null;
                RemainingEffectsCount = 0;
            }
            else
            {
                Player.AddEffect(Instantiate(Effect));
                --RemainingEffectsCount;
            }
        }
    }
}
