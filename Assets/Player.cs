﻿using UnityEngine;
using System.Collections;
using System;
using ScottGarland;

public class Player : MonoBehaviour
{
    [SerializeField]
    Card CardTemplate;

    string ScoreString { get { return Score.ToString(); } }
    public BigInteger Score = new BigInteger();

    // Use this for initialization
    void Start()
    {
        for (var i = 0; i < 25; ++i)
        {
            var card = Instantiate(CardTemplate);
            card.Place = i;
            card.transform.position = new Vector3(5-2 * (i % 5), 2.8f * (i / 5)-7, 0);
            card.Player = this;
        }
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
