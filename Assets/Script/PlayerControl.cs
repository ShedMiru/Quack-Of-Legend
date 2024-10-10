using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;

    public bool moveLeft;
    public bool dead;
    public Animator animation;
    public UnityEvent attackEvent;
    public UnityEvent passiveEvent;
    private AttackScript attackScript;
    private PassiveScript passiveScript;
    private PlayerStatus playerStatus;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackScript = FindAnyObjectByType<AttackScript>();
        passiveScript = FindAnyObjectByType<PassiveScript>();
        playerStatus = FindAnyObjectByType<PlayerStatus>();
        InvokeRepeating("UsePassive", 10f, playerStatus.haste);
        InvokeRepeating("UseAttack", 3f, playerStatus.haste * 0.3f);
    }

    void Update()
    {
        if (!dead)
        {
            Walk();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Walk()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveX * playerStatus.spd, moveY * playerStatus.spd);

        if (moveX != 0 || moveY != 0)
        {
            animation.SetBool("Moving", true);
        }
        else
        {
            animation.SetBool("Moving", false);
        }

        if (moveX > 0)
        {
            transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            moveLeft = false;
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-0.06f, 0.06f, 0.06f);
            moveLeft = true;
        }
    }

    void UsePassive()
    {
        passiveEvent?.Invoke();
        passiveScript.animation.SetBool("PassiveOn", true);
        StartCoroutine(DelayPassive());
    }
    IEnumerator DelayPassive()
    {
        yield return new WaitForSeconds(0.5945946f);
        passiveScript.animation.SetBool("PassiveOn", false);
    }

    void UseAttack()
    {
        attackEvent?.Invoke();
        StartCoroutine(AttackAnimationDelay());
    }

    IEnumerator AttackAnimationDelay()
    {
        yield return new WaitForSeconds(0.06f);
        attackScript.animation.SetBool("Attacking", true);
        StartCoroutine(DelayAttack());
    }
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.4827586f);
        attackScript.animation.SetBool("Attacking", false);
    }
}
