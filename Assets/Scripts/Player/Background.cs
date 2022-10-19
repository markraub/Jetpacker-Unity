using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] sprites;
    public int seed = 0;
    public int mapsize = 100;
    public int offset = 20;
    public float density = 50;


    private PlayerMovement player;
    private List<GameObject> tiles;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        Random.InitState(seed);
        tiles = new List<GameObject>();
        DrawTiles();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void DrawTiles()
    {
        for (int y = -mapsize; y < mapsize; y += offset)
        {
            for (int x = -mapsize; x < mapsize; x+= offset)
            {
                if (Random.Range(0, 100) <= density)
                {
                    tiles.Add(CreateTile("BGTile", x + Random.Range(-offset, offset), y + Random.Range(-offset, offset)));
                }
            }
        }
    }

    private GameObject CreateTile(string Name, float xPos, float yPos)
    {

        GameObject tile = new GameObject(Name);
        SpriteRenderer tsr = tile.AddComponent<SpriteRenderer>();
        tsr.sprite = sprites[Random.Range(0, sprites.Length)];
        tsr.color = new Color(1f, 1f, 1f, Random.Range(0.5f, 0.9f));

        tile.transform.parent = transform;
        float scaler = Random.Range(.5f, 1.6f);
        tile.transform.localRotation = new Quaternion(0, 0, Random.Range(-5, 5), 0);
        tile.transform.localScale = new Vector3(scaler, scaler, 1);
        tile.transform.position = new Vector3(xPos, yPos, Random.Range(-1, 5));

        return tile;
    }

  

 



    


    
}
