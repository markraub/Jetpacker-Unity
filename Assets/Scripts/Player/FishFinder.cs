using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFinder : MonoBehaviour
{
    public Sprite compassSprite;
    private GameObject[] fishes;
    private GameObject[] compassFishes;

    private PlayerCharacter playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        DrawFish();


    }

    // Update is called once per frame
    void Update()
    {
        if (playerCharacter.getFishCollected()){
            playerCharacter.setFishCollected(false);
            foreach (GameObject fish in compassFishes){
                Destroy(fish);
            }
            DrawFish();

        }
        for (int i = 0; i < fishes.Length; i++){
            Vector3 vectorToTarget = fishes[i].transform.position - compassFishes[i].transform.position;
            //Quaternion newRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            //compassFishes[i].transform.rotation = newRotation;
            compassFishes[i].transform.position = new Vector3(transform.position.x, transform.position.y, fishes[i].transform.position.z);
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;

            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

            compassFishes[i].transform.rotation = targetRotation;
            float distance = Mathf.Clamp(vectorToTarget.magnitude, 2, 12);
            compassFishes[i].transform.localScale = new Vector3(.1f * distance, .1f * distance, .1f * distance);


        }
        
        
    }

    private void DrawFish(){

        fishes = GameObject.FindGameObjectsWithTag("Goal");
        compassFishes = new GameObject[fishes.Length];
        
        for (int i = 0; i < compassFishes.Length; i++){
            compassFishes[i] = new GameObject("FishFinder_" + i);
            GameObject fishSprite = new GameObject("FishFinderChild_" + i);
            SpriteRenderer sr = fishSprite.AddComponent<SpriteRenderer>();
            sr.sprite = compassSprite;
            fishSprite.transform.parent = compassFishes[i].transform;
            fishSprite.transform.position += new Vector3(0, 5, 0);
            fishSprite.transform.localScale = new Vector3(.5f, .5f, .5f);
            sr.color = new Color(1f, 1f, 1f, .5f);
            //fishSprite.transform.rotation = new Quaternion(0, 0, 90, 0);
            compassFishes[i].transform.position = transform.position;



        }
    }
}
