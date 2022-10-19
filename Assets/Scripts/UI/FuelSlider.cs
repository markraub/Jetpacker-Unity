using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour
{

    private PlayerMovement player;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        player = p.GetComponent<PlayerMovement>();
        slider = GetComponent<Slider>();

        slider.maxValue = player.fuelcapacity; 
    }

    // Update is called once per frame
    void Update()
    {

        slider.SetValueWithoutNotify(player.GetCurrentFuel());
    }
}
