using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altitude : MonoBehaviour
{

    private Text t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        t.text = "ALTITUDE: " + (int)player.transform.position.y;
    }
}
