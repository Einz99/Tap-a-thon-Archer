using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomOrbitScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject image;
    public List<Sprite> sprites;  

    public void initialize()
    {
        int index = Random.Range(0, sprites.Count + 1);
        image.GetComponent<SpriteRenderer>().sprite = sprites[index];
        Destroy(GetComponent<PolygonCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.AddComponent<Rigidbody2D>();
    }

}
