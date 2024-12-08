using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MasterVolume : MonoBehaviour
{
    [Header("-------Audio Mixer-------")]

    [SerializeField] AudioMixer Master;
    [SerializeField] public Slider music_slider;
    [SerializeField] public Slider sfx_slider;

    [Header("-------Audio Sources-------")]
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource SFX;

    public AudioClip TITLESCREEN;
    public AudioClip BACKSTORY;
    public AudioClip INGAME;
    public AudioClip SHOP;

    [Header("-------SFX UI Clips-------")]

    public AudioClip CLICK;
    
    public AudioClip HOVER;

    public AudioClip GOLD;

    public AudioClip PAUSE_RESUME;

    public AudioClip GAME_OVER;

    public AudioClip PROMPT_POP;

    public AudioClip WOOD;

    public AudioClip LEAVES;

    [Header("-------SFX GAME OBJECT Clips-------")]

    public AudioClip SWOOSH;

    public AudioClip TAP;

    public AudioClip HOLD;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MenuScene")
        {
            Music.clip = TITLESCREEN;
        }

        if (scene.name == "BackStoryScene")
        {
            Music.clip = BACKSTORY;
        }

        if (scene.name == "ShopScene")
        {
            Music.clip = SHOP;
        }

        if (scene.name == "Boss1FightScene" || scene.name == "Boss2FightScene" || scene.name == "Boss3FightScene")
        {
            Music.clip = INGAME;
        }

        Music.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void play_SFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    void stop_BGM()
    {

    }

    public float musicVolume;
    public float sfxVolume;

    public void SetMusicVolume()
    {
        musicVolume = music_slider.value / 100;
        if (musicVolume == 0)
        {
            Master.SetFloat("music", -80f); // Set to the lowest dB value (effectively silent)
        }
        else
        {
            Master.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        }
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfx_slider.value / 100;
        if (sfxVolume == 0)
        {
            Master.SetFloat("sfx", -80f); // Set to the lowest dB value (effectively silent)
        }
        else
        {
           Master.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
        }
        
    }


    public void MuteAll(){
        music_slider.value = 0;
        sfx_slider.value = 0;


        // musicVolume = 0f;
        // sfx_volume = 0f;

        // Master.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        // Master.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
    }

    public void UnmuteAll(){
        music_slider.value = 100;
        sfx_slider.value = 100;

    }


    [SerializeField] private TextMeshProUGUI musicLevel;
    [SerializeField] private TextMeshProUGUI sfxLevel;
    [SerializeField] private Button muteMode;
    [SerializeField] private Button vibrateMode;

    public float music_volume;
    public float sfx_volume;

    public PlayersPreferables Settings;

    public void onConfirmSettings()
    {
        SetMusicVolume();
        SetSFXVolume();
        Settings.saveSettings(muteMode.GetComponent<Toggle>().ToggleValue, vibrateMode.GetComponent<Toggle>().ToggleValue, musicVolume * 100, sfxVolume * 100);
    }
}
