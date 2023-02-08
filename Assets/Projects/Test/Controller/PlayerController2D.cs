using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float jumpPower = 1000f;
    [SerializeField] private Vector2 backwardForce = new Vector2(-4.5f, 5.4f);
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip damageVoice;
    [SerializeField] private AudioClip[] jumpVoices;
    [SerializeField] private AudioClip clearVoice;
    [SerializeField] private AudioClip timeOverVoice;

    [SerializeField] private int playerLife = 2;
    [SerializeField] private Text lifeCountText;

    [SerializeField] private Transform playerPosition;
    [SerializeField] private float deadYLine = -15.0f;

    private Animator playerAnimator;
    private BoxCollider2D playerBoxCollider2D;
    private Rigidbody2D playerRigidbody2D;
    private bool isGround;
    private float centerY = 1.5f;
    private State state = State.Normal;
    private Vector3 defaultLocalScale;
    private AudioSource audioSource;

    [SerializeField] private GameObject sceneManagerObject;


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        defaultLocalScale = transform.localScale;
    }


    private void Update()
    {
        if (state == State.Damaged) return;

        // ?????
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);

        // ???????
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (playerLife == 0)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(timeOverVoice);

            sceneManagerObject.SendMessage("GameOver");
            enabled = false;
        }

        if (playerPosition.position.y < deadYLine)
        {
            BigLossLife();
        }
    }


    private void FixedUpdate()
    {
        isGround = CheckGround();
        playerAnimator.SetBool("isGround", isGround);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "StartUni" && state == State.Normal)
        {
            state = State.Damaged;
            StartCoroutine(OnDamage());
        }

        if (other.tag == "DamageObject" && state == State.Normal)
        {
            state = State.Damaged;
            StartCoroutine(OnDamage());
            LossLife();
        }

        if (other.tag == "GoalObject" && state == State.Normal)
        {
            state = State.Damaged;
            StartCoroutine(OnDamage());
            sceneManagerObject.SendMessage("BigLossCoin");
        }

        if (other.tag == "DeathObject" && state == State.Normal)
        {
            state = State.Damaged;
            StartCoroutine(OnDamage());
            BigLossLife();
        }
    }


    /// <summary>
    /// ??
    /// </summary>
    /// <param name="input">??</param>
    private void Move(float input)
    {
        //??????
        playerRigidbody2D.velocity = new Vector2(input * maxSpeed, playerRigidbody2D.velocity.y);

        //?????????
        if (input != 0)
        {
            float direction = Mathf.Sign(input);
            transform.localScale =
                new Vector3(defaultLocalScale.x * direction, defaultLocalScale.y, defaultLocalScale.z);
        }
        //???????
        playerAnimator.SetFloat("Horizontal", input);
        playerAnimator.SetFloat("Vertical", playerRigidbody2D.velocity.y);
        playerAnimator.SetBool("isGround", isGround);
    }


    /// <summary>
    /// ????
    /// </summary>
    private void Jump()
    {
        //?????????????
        if (isGround == true)
        {

            //????
            playerRigidbody2D.AddForce(Vector2.up * jumpPower);

            //???????
            playerAnimator.SetTrigger("Jump");

            //SE
            int i = Random.Range(0, jumpVoices.Length);
            audioSource.Stop();
            audioSource.PlayOneShot(jumpVoices[i]);
        }
    }


    /// <summary>
    /// ??????????????
    /// </summary>
    /// <returns>??????????</returns>
    private bool CheckGround()
    {
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(playerBoxCollider2D.size.x * 0.49f, 0.05f);

        bool isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);

        return isGround;
    }


    /// <summary>
    /// ?????????????
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnDamage()
    {
        // ???????????
        playerAnimator.Play(isGround ? "Damage" : "AirDamage");
        playerAnimator.Play("Idle");
        // SE
        audioSource.Stop();
        audioSource.PlayOneShot(damageVoice);

        // ??????
        playerRigidbody2D.velocity = new Vector2(2.2f * transform.right.x * backwardForce.x,
            2.2f * transform.up.y * backwardForce.y);

        // ??
        yield return new WaitForSeconds(0.1f);
        while (isGround == false)
        {
            yield return new WaitForFixedUpdate();
        }

        // ?????????
        playerAnimator.SetTrigger("Invincible Mode");
        state = State.Invincible;
    }


    /// <summary>
    /// ???????????????????
    /// </summary>
    public void OnFinishedInvincibleMode()
    {
        state = State.Normal;
    }


    /// <summary>
    /// ??????
    /// </summary>
    private enum State
    {
        Normal,
        Damaged,
        Invincible,
    }


    private void Reset()
    {
        // UnityChan2DController
        maxSpeed = 9f;
        jumpPower = 1000;
        backwardForce = new Vector2(-4.5f, 5.4f);
        whatIsGround = 1 << LayerMask.NameToLayer("Ground");

        // Transform
        transform.localScale = new Vector3(1, 1, 1);

        // Rigidbody2D
        playerRigidbody2D.gravityScale = 3.5f;

        // BoxCollider2D
        playerBoxCollider2D.size = new Vector2(1, 2.5f);
        playerBoxCollider2D.offset = new Vector2(0, -0.25f);

        // Animator
        playerAnimator.applyRootMotion = false;
    }


    public void BigLossLife()
    {
        playerLife = 0;
        lifeCountText.text = playerLife.ToString("00");
    }


    public void LossLife()
    {
        if (playerLife >= 1)
        {
            playerLife -= 1;
        }
        else
        {
            playerLife = 0;
        }

        lifeCountText.text = playerLife.ToString("00");
    }


    public void BigAddCoin()
    {
        sceneManagerObject.SendMessage("BigAddCoin");
    }


    public void AddCoin()
    {
        sceneManagerObject.SendMessage("AddCoin");
    }

    public void LossCoin()
    {
        sceneManagerObject.SendMessage("LossCoin");
    }
}
