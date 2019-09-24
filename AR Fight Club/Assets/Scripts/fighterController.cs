using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fighterController : MonoBehaviour
{

    public Transform enemyTarget;
    static  Animator anim;
    public static bool mvBack = false;
    public static bool mvFWD = false;
    public static fighterController instance;
    public static bool isAttacking = false;
    private Vector3 direction;
    public int health = 100;
    public Slider playerHB;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
        {
            direction = enemyTarget.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
        {
            isAttacking = false;
        }

        if (isAttacking == false)
        {

            if (mvBack == true)
            {
                anim.SetTrigger("wkBACK");
                anim.ResetTrigger("idle");
            }
            else
            {
                anim.SetTrigger("idle");
                anim.ResetTrigger("wkBACK");
            }

            if (mvFWD == true)
            {
                anim.SetTrigger("wkFWD");
                anim.ResetTrigger("idle");
            }
            else if (mvBack == false)
            {
                anim.SetTrigger("idle");
                anim.ResetTrigger("wkFWD");
            }

        }

    }

    public void punch()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("punch");
    }

    public void kick()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("kick");
    }

    public void react()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("react");
        health = health - 10;
        playerHB.value = health;
    }
}
