using System;
using TMPro;
using UnityEngine;

public class WinningScript : MonoBehaviour
{
    private float baseWin;
    private float timeStarted;
    private int difficulty;
    private float goldMult;
    private float expectedTime;
    public TextMeshProUGUI basereward;
    public TextMeshProUGUI timeMultxt;
    public TextMeshProUGUI diffMulttxt;
    public TextMeshProUGUI beforeReward;
    public TextMeshProUGUI GoldMultReward;
    public TextMeshProUGUI FinalReward;
    public TextMeshProUGUI WinOrLosetxt;
    public BackToShop BTS;
    private int final;
    private const string goldMultKey = "goldMultiplier";
    private const string difficultyKey = "difficulty";
    public const string currencyKey = "currency";
    void Start()
    {
        timeStarted = Time.timeSinceLevelLoad;
        difficulty = PlayerPrefs.GetInt(difficultyKey, 1);
        goldMult = 0 + (.05f * PlayerPrefs.GetInt(goldMultKey, 0));
        switch (difficulty)
        {
            case 1:
                expectedTime = 150; 
            ;break;
            case 2:
                expectedTime = 180; 
            ;break;
            case 3:
                expectedTime = 210; 
            ;break;
        }
        if (gameObject.name == "WinningPage") 
        {
            baseWin = 100;
            WinOrLosetxt.text = "You Win!";
        }
        if (gameObject.name == "LosingPage") 
        {
            baseWin = 50;
            WinOrLosetxt.text = "You Lose";
            expectedTime /= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            basereward.text = baseWin.ToString();
            float timeMult = expectedTime / timeStarted;
            timeMultxt.text = "x" + timeMult.ToString();
            diffMulttxt.text = "x" + difficulty.ToString();
            float befreward = baseWin * timeMult * difficulty;
            beforeReward.text = befreward.ToString();
            float goldExtra = befreward * goldMult;
            GoldMultReward.text = goldExtra.ToString();
            final = (int)Math.Round(befreward + goldExtra, 0);
            FinalReward.text = final.ToString();
        }    
    }

    public void onTheBackShop()
    {
        int CurFinal = final + PlayerPrefs.GetInt(currencyKey, 0);
        PlayerPrefs.SetInt(currencyKey, CurFinal);
        PlayerPrefs.Save();
        BTS.OnBackToShop();
    }
}
