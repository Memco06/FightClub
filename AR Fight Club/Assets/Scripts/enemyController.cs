using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{

    public Transform playerTransform;
    private Vector3 direction;
    static Animator anim2;
    public int enemyHealth = 100;
    public static enemyController instance;
    public Slider enemyHB;
    public BoxCollider[] c;
    public AudioClip[] auidoClip;
    AudioSource audio;
    private Vector3 enemyPosition;

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
        anim2 = GetComponent<Animator>();
        SetAllBoxColliders(false);
        audio = GetComponent<AudioSource>();
        enemyPosition = transform.position;
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
        if (anim2.GetCurrentAnimatorStateInfo(0).IsName("fight_idleCopy"))
        {
            direction = playerTransform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
            SetAllBoxColliders(false);
        }

        Debug.Log(direction.magnitude * 10);

        if (direction.magnitude * 10 > 13f && gameController.allowMovement == true)
        {
            anim2.SetTrigger("walkFWD");
            audio.Stop();
            SetAllBoxColliders(false);
        }
        else
        {
            anim2.ResetTrigger("walkFWD");
        }

        if (direction.magnitude * 10 < 13f && direction.magnitude * 10 > 8 && gameController.allowMovement == true)
        {            
            SetAllBoxColliders(true);
            if ( !audio.isPlaying  && !anim2.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick 2"))
            {
                playAudio(1);
                anim2.SetTrigger("kick");
            }
        }
        else
        {
            anim2.ResetTrigger("kick");
        }

        if (direction.magnitude * 10 < 6f && gameController.allowMovement == true)
        {
            SetAllBoxColliders(true);
            if (!audio.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("cross_punch"))
            {
                playAudio(0);
                anim2.SetTrigger("punch");
            }
        }
        else
        {
            anim2.ResetTrigger("punch");
        }

        if (direction.magnitude * 10 > 0f && direction.magnitude * 10 < 2 && gameController.allowMovement == true)
        {
            anim2.SetTrigger("walkBack");
            SetAllBoxColliders(false);
            audio.Stop();
        }
        else
        {
            anim2.ResetTrigger("walkBack");
        }
    }

    public void enemyReact()
    {
        enemyHealth = enemyHealth - 10;
        enemyHB.value = enemyHealth;
        if (enemyHealth < 10)
        {
            enemyKnockout();
            playAudio(3);
        }
        else
        {
            anim2.ResetTrigger("idle");
            anim2.SetTrigger("react");
            playAudio(2);
        }
        
    }

    public void enemyKnockout()
    {
        gameController.allowMovement = false;
        enemyHealth = 100;
        enemyHB.value = 100;
        anim2.SetTrigger("knockout");
        gameController.instance.scorePlayer();
        gameController.instance.onScreenPoints();
        gameController.instance.rounds();

        if (gameController.playerScore == 2)
        {
           gameController.instance.doReset();
        }
        else
        {
            StartCoroutine(resetCharacters());
        }
    }

    IEnumerator resetCharacters()
    {
        yield return new WaitForSeconds(4);
        GameObject[] theclone = GameObject.FindGameObjectsWithTag("Enemy");
        Transform t = theclone[1].GetComponent<Transform>();
        t.position = enemyPosition;
        t.position = new Vector3(t.position.x, 0, t.position.z);
        gameController.allowMovement = true;
    }
}
