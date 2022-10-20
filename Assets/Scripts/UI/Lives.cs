using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{

    private GameObject MainLife;
    private PlayerCharacter player;
    private GameObject[] LifeObjects;
    // Start is called before the first frame update
    void Start()
    {
        MainLife = GameObject.Find("MainLife");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        LifeObjects = new GameObject[PlayerCharacter.lives];
        LifeObjects[0] = MainLife;
        DrawLives();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerCharacter.lives != LifeObjects.Length)
        {
            for (int i = 1; i < LifeObjects.Length; i++)
            {
                Destroy(LifeObjects[i]);
            }
            LifeObjects = new GameObject[PlayerCharacter.lives];
            DrawLives();
        }
        
    }

    private void DrawLives()
    {
        for (int i = 1; i < PlayerCharacter.lives; i++)
        {
            GameObject NewLife = GameObject.Instantiate(MainLife, this.transform);
            NewLife.name = "LifeIcon_" + i;
            RectTransform img = NewLife.GetComponent<RectTransform>();
            img.localPosition += new Vector3(0, (img.rect.height * i), 0);
            NewLife.GetComponent<Image>().sprite = MainLife.GetComponent<Image>().sprite;
            LifeObjects[i] = NewLife;
        }
         

        //}
    }
}
