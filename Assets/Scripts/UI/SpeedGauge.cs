using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SpeedGauge : MonoBehaviour
{

    private PlayerMovement player;
    private RectTransform gauge;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        player = p.GetComponent<PlayerMovement>();
        gauge = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {

        float speed = player.GetVelocity().magnitude * 10.0f;
        gauge.rotation = Quaternion.Euler(gauge.rotation.x, gauge.rotation.y, GetRot(speed));

    }

    float GetRot(float speed)
    {

        float MaxSpeed = 315-45;
        if (speed <= 0)
        {
            return -45.0f;
        }
        else if ( speed >= MaxSpeed)
        {
            return -315.0f;
        }
        else
        {
            return gauge.rotation.z - speed - 45;
        }
    }
}
