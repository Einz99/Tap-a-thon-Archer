using UnityEngine;

public class RandomCameraScript : MonoBehaviour
{

    public GameObject Camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("'test'");

        switch(Random.Range(0,3)){
            case 0:
                Camera.transform.position = new Vector3(0.71f, -7.49f, 0);
            break;

            case 1:
                Camera.transform.position = new Vector3(-54.52f, -9.58f, 0);
            break;

            case 2:
                Camera.transform.position = new Vector3(5.42f, -37.19f, 0);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
