using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
 
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    public float fuelcapacity = 100;
    public float fuelburnrate = 100;
    public float fuelrecoveryrate = 5;
    public float breakspeed = 1.01f;


    private float currentfuel;
    private Rigidbody2D rb;
    private ParticleSystem jetpacksmoke;
    private AudioSource jetpacksound;
    private bool jetpackactive;
 
    
    // Start is called before the first frame update
    void Start()
    {
        
        currentfuel = fuelcapacity;
        rb = GetComponent<Rigidbody2D>();
        jetpacksmoke = GetComponent<ParticleSystem>();
        jetpacksound = GetComponent<AudioSource>();
        if (jetpacksmoke.isPlaying)
        {
            jetpacksmoke.Stop();
        }

        jetpacksound.mute = true;

    }

    // Update is called once per frame
    void Update()
    {


        float v = Input.GetAxis("Vertical");

        if (currentfuel != 0 && v > 0)
        {
            rb.AddRelativeForce(new Vector2(0, Mathf.Abs(v) * speed));
        }

        if (v > 0)
        {
            if (currentfuel != 0)
            {
                if (!jetpacksmoke.isPlaying)
                {
                    jetpacksmoke.Play();
                }

                jetpacksound.mute = false;
                jetpackactive = true;

            }

            currentfuel -= fuelburnrate * Mathf.Abs(v) / fuelcapacity;
            if (currentfuel <= 0)
            {
                currentfuel = 0;
            }
        }


        if (v <= 0)
        {
            if (jetpacksmoke.isPlaying)
            {
                jetpacksmoke.Stop();

            }
            jetpacksound.mute = true;
            jetpackactive = false;
        }


        if (Input.GetKey(KeyCode.A))
        {
            float impulse = (rotationspeed * Mathf.Deg2Rad) * rb.inertia;
            rb.AddTorque(impulse, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {            
            float impulse = (rotationspeed * Mathf.Deg2Rad) * rb.inertia * -1;
            rb.AddTorque(impulse, ForceMode2D.Impulse);          
        }

        if (Input.GetKey(KeyCode.S))
        {
            currentfuel += (Time.deltaTime * fuelrecoveryrate);
            if (currentfuel >= fuelcapacity)
            {
                currentfuel = fuelcapacity;
            }
            rb.velocity = new Vector2(rb.velocity.x / breakspeed, rb.velocity.y / breakspeed);
            rb.angularVelocity /= breakspeed;
            
        }



    }

    public float GetCurrentFuel()
    {
        return currentfuel;
    }

    public void SetCurrentFuel(float val)
    {
        currentfuel = val;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public bool isJetpackActive()
    {
        return jetpackactive;
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }

    public void AddWeight(float val)
    {
        rb.gravityScale = rb.gravityScale + val;
    }

    public void ReduceWeight(float val)
    {
        rb.gravityScale = rb.gravityScale - val;
    }



    }
