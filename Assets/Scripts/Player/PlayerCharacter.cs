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
    public AudioClip damageNoise;
    public float damageCooldown = 1;
    
    private float speed;
    private float fuel;
    private CapsuleCollider2D capsule;
    private CapsuleCollider2D[] colliding;
    private ContactFilter2D filter;
    private Rigidbody2D rb;
    private PlayerMovement player;
    private float iframeTimer = 0;
    private bool fishCollected = false;

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
        iframeTimer += Time.deltaTime;
        if (player.GetCurrentFuel() <= 0)
        {
            Debug.Log("Out of Fuel!");
            DamagePlayer(100);
            
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
                if (iframeTimer > damageCooldown){
                    DamagePlayer(10);
                }
                //spawn damage noise source
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = damageNoise;
                //clamp relative velocity to collided object between 2 and 12
                float rv = Mathf.Clamp(collision.relativeVelocity.magnitude, 2, 12);
                //remap that relative velocity between .5 and 1.5
                audioSource.pitch = (((0.5f - -0.5f) / (2 - 12)) * (rv - 12) + 0.5f);
                audioSource.loop = false;
                //Thud!
                audioSource.Play();

                Destroy(audioSource, audioSource.clip.length);
                iframeTimer = 0;
                break;
            case "Projectile":
            if (iframeTimer > damageCooldown){
                    DamagePlayer(5); 
            }
                //spawn damage noise source
                AudioSource audioSourceP = gameObject.AddComponent<AudioSource>();
                audioSourceP.clip = damageNoise;
                audioSourceP.pitch = 1.1f;
                audioSourceP.loop = false;
                audioSourceP.Play();
                Destroy(audioSourceP, audioSourceP.clip.length);
                Destroy(collision.gameObject);
                iframeTimer = 0;
                break;
            case "Goal":
            fishCollected = true;
            Destroy(collision.gameObject);
            break;

        }
            

    }

    private void DamagePlayer(float damage)
    {
        if (player.GetCurrentFuel() - damage <= 0){
            lives--;
            if (lives <= 0){
                Debug.Log("GAME OVER!");
                Application.Quit();
            }
            else {
                ReloadLevel();
            }

        }
        else {
            player.SetCurrentFuel(player.GetCurrentFuel() - damage);
        }
        

    }

    private void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public bool getFishCollected(){
        return fishCollected;
    }

    public bool setFishCollected(bool val){
        fishCollected = val;
        return fishCollected;
    }

}
