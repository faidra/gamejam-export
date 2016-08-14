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
    [SerializeField]
    Effect LimitUpEffectTest;
    [SerializeField]
    GameObject GoldenFingerSign;
    [SerializeField]
    Effect AutoAddEffectTest2;
    [SerializeField]
    GameClearEffect GameClearEffect;

    bool _started;

    public int Limit = 1;

    string ScoreString { get { return Score.ToString(); } }
    public BigInteger Score = new BigInteger();

    int? _goldenFingerPlace;
    int _maxPlace = 25;

    bool _gameCleared;

    // Use this for initialization
    void Start()
    {
        for (var i = 0; i < _maxPlace; ++i)
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
        // tekitou
        if (place == 3)
        {
            GiveCardEffect(card, Instantiate(AutoAddEffectTest), 3);
        }
        else if (place == 6)
        {
            GiveCardEffect(card, Instantiate(LimitUpEffectTest), 1);
        }
        else if (place == 7)
        {
            GiveCardEffect(card, Instantiate(AutoAddEffectTest), 2);
        }
        else if (place == 9)
        {
            GiveCardEffect(card, Instantiate(LimitUpEffectTest), 3);
        }
        else if (place == 11)
        {
            GiveCardEffect(card, Instantiate(AutoAddEffectTest2), 8);
        }
        else if (place == 14)
        {
            GiveCardEffect(card, Instantiate(LimitUpEffectTest), 4);
        }
        else if (place == 19)
        {
            GiveCardEffect(card, Instantiate(LimitUpEffectTest), 5);
        }
        else if (place == _maxPlace - 1)
        {
            GiveCardEffect(card, Instantiate(GameClearEffect), 1);
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
        return place < Limit || (_goldenFingerPlace.HasValue && place == _goldenFingerPlace);
    }

    // Update is called once per frame
    void Update()
    {
        FingerSign.transform.position = GetPositionOfPlace(Limit - 1);
        if (_started && !_gameCleared) UpdateGoldenFinger();
    }

    private void UpdateGoldenFinger()
    {
        // todo ポアソン分布
        if (_goldenFingerPlace.HasValue)
        {
            if (UnityEngine.Random.value < Time.deltaTime * 0.15)
            {
                _goldenFingerPlace = null;
                GoldenFingerSign.SetActive(false);
            }
        }
        else
        {
            if (UnityEngine.Random.value < Time.deltaTime * 0.03)
            {
                GoldenFingerSign.SetActive(true);
                _goldenFingerPlace = Mathf.Min(_maxPlace - 8, Limit + 2 + (int)(Mathf.Pow(UnityEngine.Random.value, 2) * 12)); //もうちょっとこりたい
                GoldenFingerSign.transform.position = GetPositionOfPlace(_goldenFingerPlace.Value);
            }
        }
    }

    private Vector3 GetPositionOfPlace(int place)
    {
        return new Vector3(0 - 2 * (place % 5), 2.8f * (place / 5) - 4.8f, 0);
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

    public void ExpandLimit(int num)
    {
        Limit += num;
    }

    public void GameClear()
    {
        _gameCleared = true;
        FingerSign.SetActive(false);
        GoldenFingerSign.SetActive(false);
        Limit = 100;
        Timer.Stop();
        var finger1 = Instantiate(GoldenFingerSign);
        finger1.transform.position = GetPositionOfPlace(0);
        finger1.SetActive(true);

        var finger2 = Instantiate(GoldenFingerSign);
        finger2.transform.position = GetPositionOfPlace(4);
        finger2.SetActive(true);
        finger2.transform.localScale = new Vector3(-1, 1, 1);

        var finger3 = Instantiate(GoldenFingerSign);
        finger3.transform.position = GetPositionOfPlace(20);
        finger3.SetActive(true);
        finger3.transform.localScale = new Vector3(1, -1, 1);

        var finger4 = Instantiate(GoldenFingerSign);
        finger4.transform.position = GetPositionOfPlace(24);
        finger4.SetActive(true);
        finger4.transform.localScale = new Vector3(-1, -1, 1);
    }
}
