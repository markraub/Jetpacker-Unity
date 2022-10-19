using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ProximityMine : MonoBehaviour
{
    public int Timer = 3;
    public int range = 10;
    public float power = 20;
    public Sprite ExplosionEffect;

    private TextMesh countdown;
    private PlayerMovement player;
    private AudioSource sfx;
    private bool CountdownStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        GameObject Text = new GameObject();
        Text.name = "MineText";
        Text.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Text.transform.parent = transform; 
        countdown = Text.AddComponent<TextMesh>();
        countdown.color = Color.red;
        countdown.text = Timer.ToString();
        countdown.fontSize = 28;
        countdown.alignment = TextAlignment.Center;
        countdown.anchor = TextAnchor.MiddleCenter;
        Text.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        if (distance <= range && !CountdownStarted)
        {
            StartCoroutine(Explode());
            CountdownStarted = true;


        }

    }

    private IEnumerator Explode()
    {
        for (int i = Timer; i > 0; i--)
        {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1);

        }

        Vector3 target = player.transform.position - transform.position;

        GameObject explosion = new GameObject("explosion");
        explosion.transform.parent = transform;
        explosion.transform.position = transform.position;
        Rigidbody2D erb = explosion.AddComponent<Rigidbody2D>();
        erb.gravityScale = 0;
        explosion.tag = ("Projectile");
        erb.useAutoMass = true;
        if (target.magnitude <= range)
        {
            Debug.Log(target.magnitude);
            erb.AddForce(new Vector2(target.x * power, target.y * power));
            CircleCollider2D ecc = explosion.AddComponent<CircleCollider2D>();
        }

        Destroy(explosion, 5);

        sfx.Play();
        StartCoroutine(ExplodeSprite(sfx.clip.length));
        yield return new WaitForSeconds(sfx.clip.length);

        Destroy(gameObject);


    }

    private IEnumerator ExplodeSprite(float time)
    {
        GameObject explosion = new GameObject("ExplosionEffect");
        explosion.transform.parent = transform;
        explosion.transform.position = transform.position;
        explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        explosion.transform.position += new Vector3(0, 0, -0.2f);
        SpriteRenderer es = explosion.AddComponent<SpriteRenderer>();
        es.sprite = ExplosionEffect;
        for (int i = 0; i < 20; i++)
        {
            explosion.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(time / 20);

        }
    }
}
