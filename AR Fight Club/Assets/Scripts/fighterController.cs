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
    public BoxCollider[] c;
    public AudioClip[] auidoClip;
    AudioSource audio;

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
        SetAllBoxColliders(false);
        audio = GetComponent<AudioSource>();
    }
    public void playAudio(int clip)
    {
        audio.clip = auidoClip[clip];
        audio.Play();
    }

    private void SetAllBoxColliders(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
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
            SetAllBoxColliders(false);
        }

        if (isAttacking == false)
        {

            if (mvBack == true)
            {
                anim.SetTrigger("wkBACK");
                anim.ResetTrigger("idle");
                SetAllBoxColliders(false);
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
                SetAllBoxColliders(false);
            }
            else if (mvBack == false)
            {
                anim.SetTrigger("idle");
                anim.ResetTrigger("wkFWD");
            }

        }
        else if (isAttacking == true)
        {
            SetAllBoxColliders(true);
        }

    }

    public void punch()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("punch");
        playAudio(0);
    }

    public void kick()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("kick");
        playAudio(1);
    }

    public void react()
    {
        isAttacking = true;
        health = health - 10;
        if (health < 10)
        {
            knockout();
            playAudio(3);
        }
        else
        {
            anim.ResetTrigger("idle");
            anim.SetTrigger("react");
            playAudio(2);
        }
        playerHB.value = health;
    }

    public void knockout()
    {
        anim.SetTrigger("knockout");
    }
}
