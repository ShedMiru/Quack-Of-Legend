using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public int hp, maxHp, atk, def, expCap, exp, lvlCount;
    public float spd, haste;
    public Slider slideHp;
    public Slider slideExp;
    public TextMeshProUGUI lvlText;
    public TextMeshProUGUI[] statValue;
    public GameObject[] listOfItem;
    private EnemyStatus enemyStatus;
    private PlayerControl playerControl;
    public Animator animation;
    public UnityEvent EXPCollect;
    public UnityEvent lvlUp;
    public UnityEvent playerDead;
    public UnityEvent takeDamage;


    void Start()
    {
        playerControl = FindAnyObjectByType<PlayerControl>();
        enemyStatus = FindAnyObjectByType<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        lvlText.text = lvlCount.ToString();
        statValue[0].text = atk.ToString();
        statValue[1].text = hp.ToString();
        statValue[2].text = spd.ToString();
        statValue[3].text = def.ToString();
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
            if (lvlCount < 20)
            {
                expCap += expCap * 2 / 10;
            }
            else if (lvlCount < 40)
            {
                expCap += expCap * 3 / 10;
            }
            else if (lvlCount < 75)
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
        if (col2d.tag == "Sumpit")
        {
            hp -= (enemyStatus.atk/def);
            //jangan diubah agak ngebug ke trigger 2x tapi emng niat atk*2 anggap aja fitur
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
        takeDamage?.Invoke();
        hp -= (enemyStatus.atk/def);
    }
}
