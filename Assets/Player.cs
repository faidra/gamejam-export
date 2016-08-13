﻿using UnityEngine;
using System.Collections;
using System;
using ScottGarland;

public class Player : MonoBehaviour
{
    [SerializeField]
    Card CardTemplate;

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
            card.transform.position = new Vector3(2 - 2 * (i % 5), 2.8f * (i / 5) - 4, 0);
            card.Player = this;
        }
    }

    internal bool CanAddScoreAt(int place)
    {
        return place < Limit;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.logger.Log(ScoreString);
    }

    public void AddScore(BigInteger addition)
    {
        Score += addition;
    }
}
