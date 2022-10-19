using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public float FuelPickupAmount = 25;
    static public int lives = 3;
    public float GravityPickupAmount = 0.01f;
    public float SpeedPickupAmount = 1;
    public AudioClip[] damageNoises;
    
    private float speed;
    private float fuel;
    private CapsuleCollider2D capsule;
    private CapsuleCollider2D[] colliding;
    private ContactFilter2D filter;
    private Rigidbody2D rb;
    private PlayerMovement player;

    // Start is called before the first frame update

    void Start()
    {
        filter = filter.NoFilter();
        player = GetComponent<PlayerMovement>();
        speed = player.speed;
        fuel = player.GetCurrentFuel();
        capsule = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentFuel() <= 0)
        {
            Debug.Log("Out of Fuel!");
            DamagePlayer(1);
            ReloadLevel();
            
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "FuelPickup":
                if ((player.GetCurrentFuel() + FuelPickupAmount) > 100)
                {
                    player.SetCurrentFuel(100);
                }
                else
                {
                    player.SetCurrentFuel(player.GetCurrentFuel() + FuelPickupAmount);
                }
                Destroy(collision.gameObject);
                break;
            case "LivesPickup":
                lives++;
                Destroy(collision.gameObject);
                break;
            case "SpeedPickup":
                player.SetSpeed(player.speed + SpeedPickupAmount);
                Destroy(collision.gameObject);
                break;
            case "WeightPickup":
                player.ReduceWeight(GravityPickupAmount);
                Destroy(collision.gameObject);
                break;
            case "Hostile":
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = damageNoises[Random.Range(0, damageNoises.Length)];
                audioSource.loop = false;
                audioSource.Play();
                DamagePlayer(1);
                Destroy(audioSource);
                break;
            case "Projectile":
                DamagePlayer(1);
                Destroy(collision.gameObject);
                break;

        }
            

    }

    private void DamagePlayer(int times)
    {
        for (int i = 0; i < times; i++)
        {
            lives--;
            if (lives <= 0)
            {
                lives = 3;
                ReloadLevel();
            }
             
        }

    }

    private void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
