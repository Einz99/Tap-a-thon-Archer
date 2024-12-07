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


    public void playAnimation(string type){
        anim_type = type;
        StartCoroutine(StartAnim());
    }

    public IEnumerator StartAnim()
    {
        
        if(anim_type == "close"){
            yield return new WaitForSeconds(0.5f);
            Debug.Log("playing anim");
            anim_type = "open";
        }
        
        isDone = true;

        while(isDone)
        {
            yield return new WaitForSeconds(0.05f);
            index++;
            if(index >= sprites.Count){
                index = 0;
                isDone = false;
                gameObject.SetActive(false);
            }else{
                image.sprite = sprites[index];
            }
        }
    }

}
