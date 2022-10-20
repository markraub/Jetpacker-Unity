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
        if (target.magnitude <= range)
        {
            FireLaser(target);
            //StartCoroutine(Shoot(target));
        }

        
    }

    private IEnumerator FireLaser(Vector3 target){

        if (count >= maxCount){
            yield break;
        }
        count ++;
        yield return new WaitForSeconds(shootspeed);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target, range);
        Debug.Log(hit.transform.parent.name);


    }
    private IEnumerator Shoot(Vector3 target)
    {

        if (count >= maxCount)
        {
            yield break;
        }

        count++;

        yield return new WaitForSeconds(shootspeed);
        GameObject bullet = new GameObject("bullet");
        bullet.transform.parent = transform;
        bullet.transform.position = transform.position;
        bullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        SpriteRenderer bs = bullet.AddComponent<SpriteRenderer>();
        bs.sprite = bulletsprite;
        AudioSource sfx = GetComponent<AudioSource>();
        sfx.Play();
        Rigidbody2D brb = bullet.AddComponent<Rigidbody2D>();
        brb.gravityScale = 0;
        brb.useAutoMass = true;
        bullet.tag = ("Projectile");
        brb.AddForce(new Vector2(target.x*bulletspeed, target.y*bulletspeed));
        yield return new WaitForSeconds(0.25f);
        CircleCollider2D bcc = bullet.AddComponent<CircleCollider2D>();
        bcc.radius = .5f;
        Destroy(bullet, 5);
        count--;
        
    }


}
