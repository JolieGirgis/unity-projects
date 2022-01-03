using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private Animator playerAnim;

    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private float jumpForce = 550;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver;
    public bool dashActive = false;
    private bool haveJumpedOnce = false;

    private MoveLeft moveLeftScript;
    // Start is called before the first frame update
    void Start()
    {
        // Access public variables and methods of each class
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        moveLeftScript = GameObject.Find("Background").GetComponent<MoveLeft>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // The player presses space bar to jump once when on the ground or double jump while the game is not over. A sound effect is also triggered,
        // as well as the dirt particles stop appearing from the player during the jump.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAnim.SetFloat("Speed_f", 1);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            haveJumpedOnce = true;        
        }
        // Initiate double jump
        else if (haveJumpedOnce && Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            haveJumpedOnce = false;
        }
        // While player presses the shift key, increase run animation speed and speed of moving obstacles
        while (Input.GetKey(KeyCode.LeftShift))
        {
            dashActive = true;
            playerAnim.speed = 2;
            moveLeftScript.speed += 15; 
        }
        // Default animation speed and speed of moving obstacles
        playerAnim.speed = 1;
        moveLeftScript.speed = 25;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // While player is on the ground, have dirt particles appear
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        // If player collides with obstacle, it's game over. Initiate the death animation, explosion effect, and crash sound effect. 
        // Stop dirt particle effect.
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
