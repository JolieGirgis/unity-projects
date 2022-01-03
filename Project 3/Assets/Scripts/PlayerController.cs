using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MoveLeft moveLeftScript;

    private Rigidbody playerRb;

    private Animator playerAnim;

    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private float jumpForce = 550;
    public float gravityModifier;
    public float score;
    private float increaseScorePerSecond;
    private float startDelay = 0;
    private float repeatRate = 1;

    public bool isOnGround = true;
    public bool gameOver;
    public bool dashActive = false;
    private bool haveJumpedOnce = false;
    public bool startScene = true;

    private float startValue = 0.25f;
    private float endValue = 1.60f;
    private float timeElapsed;
    private float lerpDuration = 2.6f;
    private float walkMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the score to 0 and display it to the player
        score = 0;
        Debug.Log("Score: " + score);

        // Call IncreaseScore method every 1 second to update and display the score
        InvokeRepeating("IncreaseScore", startDelay, repeatRate);

        // Access public variables and methods of each class
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        moveLeftScript = GameObject.Find("Background").GetComponent<MoveLeft>();

        // Modify gravity acting on the player
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Walking animation at the start of the game. When Speed_f is at the start value, it's the walking animation. When Speed_f gets to the endValue, it's the running animation and the game has now started
        if (timeElapsed < lerpDuration)
        {
            playerAnim.SetFloat("Speed_f", Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration));
            transform.Translate(Vector3.forward * Time.deltaTime * walkMultiplier);
            timeElapsed += Time.deltaTime;
            dirtParticle.Stop();
        }
        if (timeElapsed >= lerpDuration)
        {
            startScene = false;
        }
        // The player presses space bar to jump once when on the ground or double jump while the game is not over. A sound effect is also triggered,
        // as well as the dirt particles stop appearing from the player during the jump.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver && !startScene)
        {
            playerAnim.SetTrigger("Jump_trig");
            playerAnim.SetFloat("Speed_f", 1);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            haveJumpedOnce = true;
        }
        // Initiate double jump
        else if (haveJumpedOnce && Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && !startScene)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            haveJumpedOnce = false;
        }
        // While player presses the shift key, increase run animation speed and speed of moving obstacles
        if (Input.GetKeyDown(KeyCode.LeftShift) && !startScene)
        {
            dashActive = true;
            playerAnim.speed = 2;
        }
        // Default animation speed and speed of moving obstacles
        if (Input.GetKeyUp(KeyCode.LeftShift) && !startScene)
        {
            playerAnim.speed = 1;
            dashActive = false;
        }
    }
    private void IncreaseScore()
    {
        // While the game is not over and the start scene has finished, increase and display the score
        if (!gameOver && !startScene)
        {
            // While player is dashing, score increases twice as fast
            if (dashActive)
            {
                increaseScorePerSecond = 2;
            }
            else
            {
                increaseScorePerSecond = 1;
            }
            score += increaseScorePerSecond;
            Debug.Log("Score: " + (int)score);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // While player is on the ground and the start scene is over, have dirt particles appear
        if (collision.gameObject.CompareTag("Ground") && !startScene)
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        // If player collides with obstacle, it's game over. Initiate the death animation, explosion effect, and crash sound effect. 
        // Stop dirt particle effect.
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            dirtParticle.Stop();
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
