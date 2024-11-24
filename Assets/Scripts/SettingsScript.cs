using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI musicLevel;
    [SerializeField] private TextMeshProUGUI sfxLevel;
    [SerializeField] private Button muteMode;
    [SerializeField] private Button vibrateMode;

    public PlayersPreferables Settings;
    
    public void onConfirmSettings()
    {
        Settings.saveSettings(muteMode.GetComponent<Toggle>().ToggleValue, vibrateMode.GetComponent<Toggle>().ToggleValue, float.Parse(musicLevel.text), float.Parse(sfxLevel.text));
    }
}
