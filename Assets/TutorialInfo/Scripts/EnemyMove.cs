using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator anim;
    public GameObject rewardCoin;
    

    //Move
    public float speed;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;

    //Healt
    public int maxHealth = 30;
    private int currentHealth;

    //attack
    private bool isAttacking = false;
    public float attackCooldown = 0.5f; 

    void Start()
    {
        currentHealth = maxHealth; // Başlangıçta mevcut sağlık değerini maksimum sağlık değerine eşitle

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        FindDirection();

    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Move()
    {
        //rigid.velocity = new Vector2(speed, 0f);
        Vector2 temp = rigid.velocity;
        temp.x = speed; 
        rigid.velocity = temp; 



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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isAttacking)
        {
            Attack();

        }
        else
        {
            
            speed = -speed;
            FindDirection();
            anim.SetInteger("Status", 0);
        }


    }
    private void Attack()
    {
        anim.SetInteger("Status", 2);
        isAttacking = true;
        GameObject enemy = GameObject.Find("Enemy");


        //karaktere çarpınca enemyi durdurma
        //enemy.GetComponent<EnemyMove>().enabled = false;
        
        // Saldırı işlemleri burada gerçekleştirilir

        Invoke("ResetAttack", attackCooldown);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }






    // Düşmanın hasar alma işlemi
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Düşmanın mevcut sağlık değerinden saldırı hasarını çıkar
        Debug.Log(gameObject.name + " has taken " + damage + " damage. Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            anim.SetInteger("Status", 1);
            
            //Die(); // Eğer düşmanın sağlık değeri 0 veya daha az ise ölme fonksiyonunu çağır
            Invoke("Die", 0.5f);
            
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
}