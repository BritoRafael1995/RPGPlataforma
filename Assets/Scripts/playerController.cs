using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D playerRb;
    private Animator    playerAnimator;
    public float        speed;
    public float        jumpForce;
    public bool         isLookLeft;

    public Transform    groundCheck;
    private bool        isGrounded;
    private bool        isAttack;
    public GameObject   hitBoxPrefab;
    public Transform    mao;
    void Start()
    {

        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        _GameController.PlayerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float h         = Input.GetAxisRaw("Horizontal");

        float speedY = playerRb.velocity.y;

        Walk(h, speedY);

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        if (Input.GetButtonDown("Fire1"))
            Attack();


        playerAnimator.SetInteger("h", (int) h);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isAttack", isAttack);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Collectable")
        {
            _GameController.PlaySfx(_GameController.sfxCoin, 0.5f);
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Damage")
        {
            print("Dano");
        }
    }

    private void Walk(float direction, float speedY)
    {

        if (isAttack && isGrounded)
            direction = 0;

        if (direction > 0 && isLookLeft)
        {
            Flip();
        }
        else if (direction < 0 && !isLookLeft)
        {
            Flip();
        }

        playerRb.velocity = new Vector2(direction * speed, speedY);

    }

    private void Jump()
    {
        playerRb.AddForce(new Vector2(0, jumpForce));
        _GameController.PlaySfx(_GameController.sfxJump, 0.5f);
    }

    private void Attack()
    {
        isAttack = true;
        playerAnimator.SetTrigger("attack");
        _GameController.PlaySfx(_GameController.sfxAttack, 0.5f);
    }

    void FootStep()
    {
        _GameController.PlaySfx(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 0.8f);
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;

        float x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void onEndedAttack()
    {
        isAttack = false;
    }
    
    void hitBoxAttack()
    {
        GameObject hitBoxTemp = Instantiate(hitBoxPrefab, mao.position, transform.localRotation);
        Destroy(hitBoxTemp, 0.2f);
    }
}
