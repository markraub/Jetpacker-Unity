using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{

    public Sprite[] asteroids;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = asteroids[Random.Range(0, 4)];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
