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

    public AudioClip PROMPT_POP;

    public AudioClip WOOD;

    public AudioClip LEAVES;

    public AudioClip GAME_OVER;

    public AudioClip VICTORY;

    [Header("-------SFX GAME OBJECT Clips-------")]

    public AudioClip SWOOSH;
    public AudioClip TAP;
    public AudioClip HOLD;
    public AudioClip PLAYER_DAMAGED;

    [Header("-------SFX BEAR -------")]
    public AudioClip BEAR_CLAW;
    public AudioClip BEAR_KICK;
    public AudioClip BEAR_STOMP;
    public AudioClip BEAR_THROW;
    public AudioClip BEAR_TRANSFORM;
    public AudioClip BEAR_DAMAGED;

    [Header("-------SFX GOLEM -------")]
    public AudioClip GOLEM_CHARGE;
    public AudioClip GOLEM_STOMP_1;
    public AudioClip GOLEM_STOMP_2;
    public AudioClip GOLEM_TRANSFORM;
    public AudioClip GOLEM_DAMAGED;

    [Header("-------SFX SLIME -------")]
    public AudioClip SLIME_WHIP;
    public AudioClip SLIME_SHOOT;
    public AudioClip SLIME_BLOW;
    public AudioClip SLIME_RIPPLE;
    public AudioClip SLIME_BOUNCE;
    public AudioClip SLIME_TRANSFORM;
    public AudioClip SLIME_DAMAGED;

    [Header("-------SFX PROJECTILES -------")]

    public AudioClip ARROW_HIT;
    public AudioClip SHOOTING_ARROW;

    public AudioClip CRIT_ARROW;

    public AudioClip BOW_CHARGE;
    

    public AudioClip WIND_CLAW;
    public AudioClip BULLET_ROCK;
    public AudioClip MEGA_ROCK;
    public AudioClip ROCK_MAGMA;
    public AudioClip BIG_MAGMA;
    public AudioClip FIRE_CLAW;
    public AudioClip GROUND_SHOCKWAVE;

    public AudioClip FLOATING_ROCK;
    public AudioClip ROCK_SHIELD;
    public AudioClip CRYSTAL_SPIKE;


    public AudioClip SLIME_SPLASH;
    public AudioClip SLIME_BULLET;
    public AudioClip BUBBLE_POP;


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
