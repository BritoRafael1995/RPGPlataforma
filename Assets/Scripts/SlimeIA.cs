using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIA : MonoBehaviour
{
    private GameController  _gameController;
    private Rigidbody2D     slimeRB;
    private Animator        slimeAnimator;

    public  float           speed;
    public  float           timeToWalk;
    public  GameObject      hitBox;
    public  GameObject      coinPrefab;

    private int             h;
    public  bool            isLookLeft;

    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        slimeRB         = GetComponent<Rigidbody2D>();
        slimeAnimator   = GetComponent<Animator>();

        StartCoroutine("SlimeWalk");
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        if (h != 0)
            slimeAnimator.SetBool("isWalking", true);
        else
            slimeAnimator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "hitBox")
        {
            h = 0;
            StopCoroutine("SlimeWalk");
            Destroy(hitBox);
            _gameController.PlaySfx(_gameController.sfxEnemyDead, 0.2f);
            slimeAnimator.SetTrigger("dead");
            Instantiate(coinPrefab, this.gameObject.transform.position, transform.localRotation);
        }
    }

    IEnumerator SlimeWalk()
    {
        int rand = Random.Range(0, 100);

        if (rand < 33)
            h = -1;
        else if (rand < 66)
            h = 0;
        else
            h = 1;

        yield return new WaitForSeconds(timeToWalk);
        StartCoroutine("SlimeWalk");
    }

    private void Walk()
    {

        if (h > 0 && isLookLeft)
        {
            Flip();
        }
        else if (h < 0 && !isLookLeft)
        {
            Flip();
        }

        slimeRB.velocity = new Vector2(h * speed, slimeRB.velocity.y);

    }

    void Flip()
    {
        isLookLeft = !isLookLeft;

        float x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void OnDead()
    {
        Destroy(this.gameObject);
    }
}
