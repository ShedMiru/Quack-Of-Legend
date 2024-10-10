using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int hp, maxHp, atk, expCap, exp, lvlCount;
    public float spd, haste;
    public Slider slideHp;
    public Slider slideExp;
    private EnemyStatus enemyStatus;
    private PlayerControl playerControl;
    public Animator animation;
    public UnityEvent EXPCollect;
    public UnityEvent lvlUp;
    public UnityEvent playerDead;
    

    void Start()
    {
        playerControl = FindAnyObjectByType<PlayerControl>();
        enemyStatus = FindAnyObjectByType<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        slideHp.maxValue = maxHp;
        slideHp.value = hp;
        slideExp.maxValue = expCap;
        slideExp.value = exp;
        if (hp <= 0 && playerControl.dead == false)
        {
            playerControl.dead = true;
            playerControl.CancelInvoke("UsePassive");
            playerControl.CancelInvoke("UseAttack");
            animation.SetBool("Dead", true);
            StartCoroutine(DeadAnimationDelay());
        }
        if (exp >= expCap)
        {
            lvlUp?.Invoke();
            int expCapTemp = expCap;
            if (lvlCount < 10)
            {
                expCap += expCap * 2 / 10;
            }
            else if (lvlCount < 20)
            {
                expCap += expCap * 3 / 10;
            }
            else if (lvlCount < 35)
            {
                expCap += expCap * 4 / 10;
            }
            else
            {
                expCap += expCap * 5 / 10;
            }
            exp -= expCapTemp;
            lvlCount++;
        }
    }
    IEnumerator DeadAnimationDelay()
    {
        yield return new WaitForSeconds(0.7540984f);
        animation.SetBool("Dead", false);
        playerDead?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D col2d)
    {
        if (col2d.tag == "Enemy")
        {
            InvokeRepeating("TakeDamage", 0f, 0.4f);
        }
        if (col2d.tag == "EXP")
        {
            EXPCollect?.Invoke();
            exp += Random.Range(2, 5);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            CancelInvoke("TakeDamage");
        }
    }
    void TakeDamage()
    {
        hp -= enemyStatus.atk;
    }
}
