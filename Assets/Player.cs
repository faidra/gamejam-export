using UnityEngine;
using System.Collections;
using System;
using ScottGarland;

public class Player : MonoBehaviour
{
    [SerializeField]
    Card CardTemplate;
    [SerializeField]
    Timer Timer;
    [SerializeField]
    EffectManager EffectManager;
    [SerializeField]
    Effect AutoAddEffectTest;
    [SerializeField]
    GameObject FingerSign;

    bool _started;

    public int Limit = 1;

    string ScoreString { get { return Score.ToString(); } }
    public BigInteger Score = new BigInteger();

    // Use this for initialization
    void Start()
    {
        for (var i = 0; i < 25; ++i)
        {
            var card = Instantiate(CardTemplate);
            card.Place = i;
            card.transform.position = GetPositionOfPlace(i);
            card.Player = this;

            GiveCardEffect(card, i);
        }
    }

    private void GiveCardEffect(Card card, int place)
    {
        // todo
        if (place == 3)
        {
            GiveCardEffect(card, Instantiate(AutoAddEffectTest), 3);
        }
    }

    private void GiveCardEffect(Card card, Effect effectInstance, int count)
    {
        card.Effect = effectInstance;
        card.Effect.transform.SetParent(card.transform);
        card.Effect.transform.localPosition = Vector3.zero;
        card.RemainingEffectsCount = count;
    }

    internal bool CanAddScoreAt(int place)
    {
        return place < Limit;
    }

    // Update is called once per frame
    void Update()
    {
        FingerSign.transform.position = GetPositionOfPlace(Limit - 1);
    }

    private Vector3 GetPositionOfPlace(int place)
    {
        return new Vector3(2 - 2 * (place % 5), 2.8f * (place / 5) - 4, 0);
    }

    public void AddScore(BigInteger addition)
    {
        Score += addition;
        if (!_started)
        {
            _started = true;
            Timer.StartsCount();
        }
    }

    public void UseScore(BigInteger cost)
    {
        Score -= cost;
    }

    public void Add2Power(int place)
    {
        AddScore(new BigInteger(1) << place);
    }

    public void AddEffect(Effect effect)
    {
        EffectManager.AddEffect(effect);
    }
}
