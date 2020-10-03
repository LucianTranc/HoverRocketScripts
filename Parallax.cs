using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //https://www.youtube.com/watch?v=zit45k6CUMk&t=403s

    public GameObject cam;
    public float parallaxAmount;
    private float startposx, startposy;
    // Start is called before the first frame update
    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(startposx + cam.transform.position.x * parallaxAmount, startposy + cam.transform.position.y * parallaxAmount, transform.position.z);

    }
}
