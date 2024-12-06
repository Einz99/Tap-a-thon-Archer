using UnityEngine;

public class MasterVolume : MonoBehaviour
{
    public AudioSource TITLESCREEEN_BGM;
    public AudioSource SFX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void play_titlescreen_BGM(){
        TITLESCREEEN_BGM.Play();
    }

    void play_SFX(string sfx_name){
        SFX.PlayOneShot((AudioClip)Resources.Load("SFX/"+sfx_name));
    }

    void stop_BGM(){

    }
}
