using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Enemy_Patron : MonoBehaviour
{

    public float speed;
    public Transform left, right;
    public GameObject rewardCoin;

    //Healt
    public int maxHealth = 30;
    private int currentHealth;
    public Slider healtSlider;

    //attack
    private bool isAttacking = false;
    public float attackCooldown = 0.2f;


    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private bool turn;
    private float currentSpeed;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Başlangıçta mevcut sağlık değerini maksimum sağlık değerine eşitle
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FindDirection();
        turn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        TurnEnemy();
        Debug.Log(currentHealth);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isAttacking)
        {
            Attack();

        }
       


    }
    private void Attack()
    {
        anim.SetInteger("Status", 3);
        isAttacking = true;
        GameObject enemy = GameObject.Find("Enemy");


        //karaktere çarpınca enemyi durdurma
        //enemy.GetComponent<EnemyMove>().enabled = false;

        Invoke("ResetAttack", attackCooldown);
    }

    private void ResetAttack()
    {
        isAttacking = false;
        
    }

    // Düşmanın hasar alma işlemi
    public void TakeDamage(int damage)
    {
        //currentHealth -= damage; // Düşmanın mevcut sağlık değerinden saldırı hasarını çıkar
        Debug.Log(gameObject.name + " has taken " + damage + " damage. Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            anim.SetInteger("Status", 1);
            

            //Die(); // Eğer düşmanın sağlık değeri 0 veya daha az ise ölme fonksiyonunu çağır
            Invoke("Die", 0.5f);

        }
        else if (currentHealth > 0)
        {
            currentHealth-=damage;
            healtSlider.value = currentHealth;
            
        }
    }

    // Düşmanın ölme işlemi
    private void Die()
    {
        Debug.Log(currentHealth);
       

        Instantiate(rewardCoin, this.gameObject.transform.position, Quaternion.identity);
        // Düşmanın ölme animasyonunu oynatabilir, ses çalabilir veya başka işlemler yapabilirsiniz
        Destroy(gameObject); // Düşman oyun nesnesini yok et
        AudioController.instance.enemyDieSound(gameObject.transform.position);
    }





    private void FindDirection()
    {
        if (speed < 0)
        {
            sprite.flipX = true;

        }
        else if (speed > 0)
        {
            sprite.flipX = false;
        }

    }


    private void MoveEnemy()
    {
        rigid.velocity = new Vector2(speed, 0f);
    }

    private void TurnEnemy()
    {
        if(!sprite.flipX && transform.position.x >= right.position.x)
        {
            

            if (turn)
            {
                turn = false;
                currentSpeed = speed;
                speed = 0;
                StartCoroutine("TurnLeft", currentSpeed);
            }

        }
        else if(sprite.flipX && transform.position.x <= left.position.x)
        {
           
            if (turn)
            {
                turn = false;
                currentSpeed = speed;
                speed = 0;
                StartCoroutine("TurnRight", currentSpeed);
            }

        }
    }


    IEnumerator TurnLeft(float currentSpeed)
    {
        anim.SetInteger("Status", 2);
        yield return new WaitForSeconds(2f);
        anim.SetInteger("Status",0);
        sprite.flipX = true;
        speed = -currentSpeed;
        turn = true;
    }
    IEnumerator TurnRight(float currentSpeed)
    {
        anim.SetInteger("Status", 2);
        yield return new WaitForSeconds(2f);
        anim.SetInteger("Status", 0);
        sprite.flipX = false;
        speed = -currentSpeed;
        turn = true;
    }



}
