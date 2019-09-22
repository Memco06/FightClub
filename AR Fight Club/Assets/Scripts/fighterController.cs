using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighterController : MonoBehaviour
{

    public Transform enemyTarget;
    Animator anim;
    public static bool mvBack = false;
    public static bool mvFWD = false;
    public static fighterController instance;

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
