using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret : MonoBehaviour
{
    public float shootspeed = 0.5f;
    public float range = 5;
    public Sprite bulletsprite;
    public float bulletspeed = 200;
    public Material lineMat;
    public Color laserColor;

    private PlayerMovement player;
    private int count = 0;
    private int maxCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target, range);
        if (hit.collider != null)
        {
            StartCoroutine(FireLaser(hit));

        }



    }

    private IEnumerator FireLaser(RaycastHit2D hit){

        if (count >= maxCount){
            yield break;
        }
        count ++;
        yield return new WaitForSeconds(shootspeed);

        LineRenderer lr = gameObject.AddComponent<LineRenderer>();
        Destroy(lr, shootspeed * .9f);

        lr.positionCount = Random.Range(5, 20);
        lr.alignment = LineAlignment.View;

        Vector3[] laserPoints = new Vector3[lr.positionCount];
        laserPoints[0] = transform.position;
        laserPoints[laserPoints.Length - 1] = hit.collider.transform.position;

        lr.material = lineMat;
        lr.startColor = laserColor;
        lr.endColor = laserColor;
        lr.startWidth = 0.3f;
        lr.endWidth = 0.05f;
        Debug.Log("Laser Hit!");

        while (lr != null)
        {
            for (int i = 1; i < laserPoints.Length - 1; i++)
            {
                Vector3 location = Vector3.Lerp(transform.position, hit.collider.transform.position, i / laserPoints.Length - 1);

                laserPoints[i] = location + new Vector3(Mathf.PerlinNoise(location.x, location.y), Mathf.PerlinNoise(location.x, location.y), 0);
            }
            laserPoints[laserPoints.Length - 1] = hit.collider.transform.position;
            lr.SetPositions(laserPoints);


        }
        
        count--;

    }


    
    


}
