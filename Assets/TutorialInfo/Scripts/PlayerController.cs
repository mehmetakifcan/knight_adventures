using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isGrounded;
    public float jumpDelay;
    private bool doubleJump;
    private bool isJumping;
    public Transform feet;
    public float feetradius;
    public LayerMask myLayer;
    public float width, height;

    public float speed;
    public float jumpforce;

    public bool leftClicked;
    public bool rightClicked;

 


    //player attack

    public float attackRange = 1.5f; // Saldırı menzili
    public LayerMask enemyLayers; // Düşman katmanı

    public Transform attackPoint; // Saldırı noktası
    public int damage = 10; // Saldırı hasarı






    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
       
    }

    private void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(feet.position, feetradius, myLayer);
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(width, height), 360f, myLayer);



        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            MovePlayer(h);
        }
        else
        {
            StopPlayer();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        PlayerFall();

        
        if (Input.GetKeyDown(KeyCode.Space)) // Klavyede Space tuşuna basıldığında
         {
                Attack(); // Saldırı fonksiyonunu çağır

                

               
         }


        if (leftClicked)
        {
            MovePlayer(-1f);

            UnityEngine.Debug.Log("-1");
        }
        else if (rightClicked)
        {
            MovePlayer(1f);

            UnityEngine.Debug.Log("1");
        }





    }
    //player move controller
    private void MovePlayer(float h)
    {
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

        if (h < 0)
        {
            sprite.flipX = true;
        }
        else if (h>0)
        {
            sprite.flipX = false;
        }
        if (!isJumping)
        {
            anim.SetInteger("Status", 1);
        }
        

    }
    private void PlayerFall()
    {
        if (rigid.velocity.y < 0)
        {
            anim.SetInteger("Status", 3);
        }
    }
    private void Jump()
    {
        if (isGrounded)
        {
            //AudioController.instance.jumpSound(gameObject.transform.position);
            isJumping = true;
            rigid.AddForce(new Vector2(0f, jumpforce));
            if (isJumping)
            {
                anim.SetInteger("Status", 2);
                AudioController.instance.jumpSound(gameObject.transform.position);
                Invoke("DoubleJump", jumpDelay);
            }
        }
        if (doubleJump && !isGrounded)
            {
                //AudioController.instance.jumpSound(gameObject.transform.position);
                rigid.velocity = Vector2.zero;
                rigid.AddForce(new Vector2(0f, jumpforce));
                anim.SetInteger("Status", 2);
                AudioController.instance.jumpSound(gameObject.transform.position);

            doubleJump = false;
            }
        

    }
    private void DoubleJump()
    {
        doubleJump = true;
    }
    private void StopPlayer()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (!isJumping)
        {
            anim.SetInteger("Status", 0);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            GameController.instance.PlayerHit(gameObject);
        }
        

        if (collision.gameObject.CompareTag("RewardCoin"))
        {
            GameController.instance.MushroomCount();
            Destroy(collision.gameObject);
            AudioController.instance.coinSound(gameObject.transform.position);

            GameController.instance.ScoreCount(4);
        }

        if (collision.gameObject.CompareTag("BossKey"))
        {
            
            GameController.instance.DisableWall();
        }

    }
    


    private void Attack()
    {
        anim.SetInteger("Status", 4);
        AudioController.instance.attackSound(gameObject.transform.position);
        // Düşmanları tespit etmek için bir overlap circle oluşturulur
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
       
        // Her tespit edilen düşman için saldırı yap
        foreach (Collider2D enemy in hitEnemies)
        {
            // Düşmana zarar ver
            EnemyMove enemyHealth = enemy.GetComponent<EnemyMove>();


            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                
            }
            // Burada düşmana zarar verildiğinde veya başka işlemler yapılabilir
            UnityEngine.Debug.Log("Düşmanı vurdun: " + enemy.name);
        }
        foreach (Collider2D Patron_enemy in hitEnemies)
        {
            // Düşmana zarar ver
            Enemy_Patron enemyHealth = Patron_enemy.GetComponent<Enemy_Patron>();


            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);

            }
            // Burada düşmana zarar verildiğinde veya başka işlemler yapılabilir
            UnityEngine.Debug.Log("Düşmanı vurdun: " + Patron_enemy.name);
        }

    }

    // Gizli olarak çizilen saldırı menzili gösterilir (geliştirme amaçlı)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void MoveLeftMobile()
    {
        leftClicked = true;
        rightClicked = false;
    }

    public void MoveRightMobile()
    {

        rightClicked = true;
        leftClicked = false;
    }

    public void StopPlayerMobile()
    {
        leftClicked = false;

        rightClicked = false;


    }
    public void JumpMobile()
    {
        Jump();
    }
    public void AttackMobile()
    {
        Attack();
    }

}
