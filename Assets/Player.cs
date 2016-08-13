using UnityEngine;
using System.Collections;
using System;
using ScottGarland;

public class Player : MonoBehaviour
{
    string ScoreString { get { return Score.ToString(); } }
    public BigInteger Score = new BigInteger();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.logger.Log(ScoreString);
    }
}
