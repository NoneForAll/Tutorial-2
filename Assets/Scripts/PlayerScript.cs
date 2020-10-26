using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text gameEnd;
    public Text life;
    private int scoreValue = 0;
    private int lifeValue = 3;
    public Transform TP;
    Animator Anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        life.text = "Health: " + lifeValue.ToString();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if(facingRight == false && hozMovement > 0)
        {
            Flip();
        } 

        if(facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if(hozMovement > 0 || hozMovement < 0)
        {
            Anim.SetInteger("State", 1);
        }

        if(vertMovement == 0 && hozMovement == 0)
        {
            Anim.SetInteger("State", 0);
        }

        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Update()
    {
 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue++;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if(scoreValue == 5)
            {
                transform.position = new Vector2(94.56f, 0.69f);
                lifeValue = 3;
                life.text = "Health: " + lifeValue.ToString();
            }

            if(scoreValue >= 10)
            {
                gameEnd.text = "You Win! \n Game Created by Alejandro A.";
            }
        }

        if(collision.collider.tag == "Hazard")
        {
            lifeValue--;
            life.text = "Health: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);

            if(lifeValue <= 0)
            {
                gameEnd.text = "You Lose! \n Better luck next time!";
                Destroy(this);
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            }
        }
    }
}
