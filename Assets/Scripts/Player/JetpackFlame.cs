using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackFlame : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<PlayerMovement>().isJetpackActive())
        {
            
            transform.localScale = new Vector3(Random.Range(0.25f, 0.35f), Random.Range(0.25f, 0.35f), Random.Range(0.25f, 0.35f));

        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }



    }


}
