using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScriptLoading : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Button mute;
    [SerializeField] public Button vibrate;
    [SerializeField] public Slider musicLevel;
    [SerializeField] public Slider sfxLevel;
    [SerializeField] public TextMeshProUGUI musicValue;
    [SerializeField] public TextMeshProUGUI sfxValue;

    // Settings Keys
    public const string muteKey = "muteMode";
    public const string vibrateKey = "vibrateMode";
    public const string musicLevelKey = "musicLevel";
    public const string sfxLevelKey = "sfxLevel";
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        mute.GetComponent<Toggle>().ToggleValue = PlayerPrefs.GetInt(muteKey) == 1;
        vibrate.GetComponent<Toggle>().ToggleValue = PlayerPrefs.GetInt(vibrateKey) == 1;
        musicLevel.GetComponent<Slider>().value = PlayerPrefs.GetFloat(musicLevelKey);
        sfxLevel.GetComponent<Slider>().value = PlayerPrefs.GetFloat(sfxLevelKey);
        musicValue.text = PlayerPrefs.GetFloat(musicLevelKey).ToString();
        sfxValue.text = PlayerPrefs.GetFloat(sfxLevelKey).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
