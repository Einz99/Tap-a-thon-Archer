using System;
using UnityEngine;

public class PlayersPreferables : MonoBehaviour
{
    // Settings Keys
    public const string muteKey = "muteMode";
    public const string vibrateKey = "vibrateMode";
    public const string musicLevelKey = "musicLevel";
    public const string sfxLevelKey = "sfxLevel";
    // ---------------------
    // Player stats Keys;
    public const string currencyKey = "currency";
    public const string maxHealthKey = "maxHealth";
    public const string attackPowerKey = "attackPower";
    public const string attackSpeedKey = "attackSpeed";
    public const string moveSpeedKey = "moveSpeed";
    // ---------------------
    // Scenes Checker;

    // ---------------------
    // Settings Value
    public bool muteMode;
    public bool vibrateMode;
    public float musicLevel;
    public float sfxLevel;
    // ---------------------
    // Player Stats Value;
    public int currency;
    public int maxHealth;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;

    void Start()
    {
        loadSettings();
        loadStats();
    }

    public void saveSettings(bool muteMode, bool vibrateMode, float musicLevel, float sfxLevel) 
    {
        PlayerPrefs.SetInt(muteKey, muteMode ? 1 : 0);
        PlayerPrefs.SetInt(vibrateKey, vibrateMode ? 1 : 0);
        PlayerPrefs.SetFloat(musicLevelKey, musicLevel);
        PlayerPrefs.SetFloat(sfxLevelKey, sfxLevel);

        PlayerPrefs.Save();
    }
    
    public void saveStats(int currency, int maxHealth, float attackPower, float attackSpeed, float moveSpeed)
    {
        PlayerPrefs.SetInt(currencyKey, currency);
        PlayerPrefs.SetInt(maxHealthKey, maxHealth);
        PlayerPrefs.SetFloat(attackPowerKey, attackPower);
        PlayerPrefs.SetFloat(attackSpeedKey, attackSpeed);
        PlayerPrefs.SetFloat(moveSpeedKey, moveSpeed);

        PlayerPrefs.Save();
    }

    public void loadSettings() 
    {
        muteMode = PlayerPrefs.GetInt(muteKey, 1) == 1;
        vibrateMode = PlayerPrefs.GetInt(vibrateKey, 1) == 1;
        musicLevel = PlayerPrefs.GetFloat(musicLevelKey, 100);
        sfxLevel = PlayerPrefs.GetFloat(sfxLevelKey, 100);
    }

    public void loadStats ()
    {
        currency = PlayerPrefs.GetInt(currencyKey, 0);
        maxHealth = PlayerPrefs.GetInt(maxHealthKey, 5);
        attackPower = PlayerPrefs.GetFloat(attackPowerKey, 10f);
        attackSpeed = PlayerPrefs.GetFloat(attackSpeedKey, 1f);
        moveSpeed = PlayerPrefs.GetFloat(moveSpeedKey, 1f);
    }

    
}
