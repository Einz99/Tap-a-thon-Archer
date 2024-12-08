using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUISprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image image;
    public List<Sprite> sprites;
    public float animSpeed = 0.1f;
    private int index;
    private bool isDone;

    private string anim_type;

    public string object_type;

    public void playAnimation(string type){
        anim_type = type;
        StartCoroutine(StartAnim());

    }

    public void setObjectType(string type){
        object_type =  type;
    }

    public MasterVolume sfx;

    public IEnumerator StartAnim()
    {

        if(anim_type == "close"){
            yield return new WaitForSecondsRealtime(0.5f);
            Debug.Log("playing anim");
            anim_type = "open";
        }
        
        if(object_type == "leaves"){
            sfx.play_SFX(sfx.LEAVES);
        }
        isDone = true;

        while(isDone)
        {
            Debug.Log("Start anim");
            yield return new WaitForSecondsRealtime(0.05f);
            index++;
            if(index >= sprites.Count){
                Debug.Log("Stop anim");
                index = 0;
                isDone = false;
                gameObject.SetActive(false);
            }else{
                Debug.Log("Play anim");
                image.sprite = sprites[index];
            }
        }
    }

    [SerializeField] public bool onLoop;

    public void stopLoopedAnimation(){
        onLoop =  false;
    }

    public void playLoopedAnimaion(){
        onLoop = true;
        StartCoroutine(startLoopedAnimation());
    }

    public IEnumerator startLoopedAnimation(){
        while(onLoop)
            {
                yield return new WaitForSecondsRealtime(animSpeed);
                index++;
                if(index >= sprites.Count){
                    index = 0;
                }else{
                    image.sprite = sprites[index];
                }
            }
    }

}
