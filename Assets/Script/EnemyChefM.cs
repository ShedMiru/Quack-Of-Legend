using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChefM : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject playerObject;
    GameObject bone;
    public GameObject maxPos;
    public GameObject minPos;
    public GameObject exp;
    public UnityEvent hitEvent;

    int hp;
    public int addKnifeDmg, addFeatherDmg;

    public bool moveLeft;
    public bool followBone;
    public bool dead;
    public Animator animation;
    BoxCollider2D boxCol2d;
    private PlayerStatus playerStatus;
    private GameState gameState;
    private EnemyStatus enemyStatus;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStatus = FindAnyObjectByType<PlayerStatus>();
        gameState = FindAnyObjectByType<GameState>();
        enemyStatus = FindAnyObjectByType<EnemyStatus>();
        hp = enemyStatus.hp;
    }

    void Update()
    {
        Walk();
        if (hp <= 0 && !dead)
        {
            boxCol2d = GetComponent<BoxCollider2D>();
            boxCol2d.enabled = false;
            dead = true;
            animation.SetBool("Dead", true);
            StartCoroutine(DelayDead());
        }
    }

    void Walk()
    {
        playerObject = GameObject.Find("Player");
        if (gameState.paused || gameState.gameover)
        {
            rb.velocity = new Vector2(0, 0);
            animation.speed = 0f;
        }
        else
        {
            animation.speed = 1f;
            if (playerObject.transform.position.x > transform.position.x && !followBone)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                moveLeft = false;
            }
            else if (playerObject.transform.position.x < transform.position.x && !followBone)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                moveLeft = true;
            }
            if (followBone && !dead)
            {
                bone = GameObject.Find("Bone(Clone)");
                transform.position = Vector2.MoveTowards(transform.position, bone.transform.position, enemyStatus.spd * Time.deltaTime);
                animation.SetBool("Moving", true);
                if (bone.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    moveLeft = false;
                }
                else if (bone.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    moveLeft = true;
                }
            }
            else if (!dead)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, enemyStatus.spd * Time.deltaTime);
                animation.SetBool("Moving", true);
            }
            else
            {
                animation.SetBool("Moving", false);
            }
        }
    }

    IEnumerator DelayDead()
    {
        yield return new WaitForSeconds(0.25f);
        EXPDrop();
        animation.SetBool("Dead", false);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col2d)
    {
        if (col2d.tag == "Attack")
        {
            hp -= playerStatus.atk + addFeatherDmg;
            hitEvent?.Invoke();
        }
        if (col2d.tag == "Passive")
        {
            hp -= playerStatus.atk / 2;
            hitEvent?.Invoke();
        }
        if (col2d.tag == "Knife")
        {
            hp -= playerStatus.atk / 2 + addKnifeDmg;
            hitEvent?.Invoke();
        }
        if (col2d.tag == "BoneRadius")
        {
            followBone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "BoneRadius")
        {
            followBone = false;
        }
    }

    void EXPDrop()
    {
        GameObject expDrop = Instantiate(exp);
        expDrop.transform.position = transform.position;
    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.collider.CompareTag("Player"))
    //     {
    //         Col2d.isTrigger = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.tag == "Player")
    //     {
    //         Col2d.isTrigger = false;
    //     }
    // }
}
