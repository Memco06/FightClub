﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class gameController : MonoBehaviour
{

    public static gameController instance;
    public static bool allowMovement = false;
    public GameObject flashButton;
    public GameObject cameraButton;
    public GameObject playerScoreOnScreen;
    public GameObject enemyScoreOnScreen;
    public GameObject backButton;
    public GameObject fwdButton;
    public GameObject punchButton;
    public GameObject kickButton;
    private bool played321 = false;
    public AudioClip[] audioClip;
    AudioSource audio;
    public static int playerScore = 0;
    public static int enemyScore = 0;
    public GameObject[] points;
    public static int round = 0;



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
        audio = GetComponent<AudioSource>();
    }

    private void playAudioTrack(int clip)
    {
        audio.clip = audioClip[clip];
        audio.Play();
    }

    public void scorePlayer()
    {
        playerScore++;
    }

    public void scoreEnemy()
    {
        enemyScore++;
    }

    public void doReset()
    {
        if (playerScore == 2)
        {
            playAudioTrack(6);
        }
        else
        {
            playAudioTrack(5);
        }
        fighterController.instance.playerHB.value = 100;
        fighterController.instance.health = 100;

        enemyController.instance.enemyHB.value = 100;
        enemyController.instance.enemyHealth = 100;
        playerScore = 0;
        enemyScore = 0;       
        StartCoroutine(restartGame());
    }

    IEnumerator restartGame()
    {
        yield return new WaitForSeconds(4.5f);
        points[0].SetActive(false);
        points[1].SetActive(false);
        points[2].SetActive(false);
        points[3].SetActive(false);
        allowMovement = true;
        StartCoroutine(restartRoundAudio());
    }

    IEnumerator restartRoundAudio()
    {
        yield return new WaitForSeconds(2);
        playAudioTrack(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (played321 == false)
        {
            if (DefaultTrackableEventHandler.isCamera)
            {
                flashButton.SetActive(false);
                cameraButton.SetActive(false);
                playerScoreOnScreen.SetActive(true);
                enemyScoreOnScreen.SetActive(true);
                backButton.SetActive(true);
                fwdButton.SetActive(true);
                punchButton.SetActive(true);
                kickButton.SetActive(true);
                played321 = true;
                StartCoroutine(round1());
            }
        }
    }

    IEnumerator round1()
    {
        yield return new WaitForSeconds(0);
        playAudioTrack(0);
        StartCoroutine(prepareYourself());
    }

    IEnumerator prepareYourself()
    {
        yield return new WaitForSeconds(1.2f);
        playAudioTrack(1);
        StartCoroutine(start321());
    }

    IEnumerator start321()
    {
        yield return new WaitForSeconds(2f);
        playAudioTrack(2);
        StartCoroutine(allowPlayerMovement());
    }

    IEnumerator allowPlayerMovement()
    {
        yield return new WaitForSeconds(5f);
        allowMovement = true;
    }

    public void onScreenPoints()
    {
        if (playerScore == 1)
        {
            points[0].SetActive(true);
        }
        else if (playerScore == 2)
        {
            points[1].SetActive(true);
        }

        if (enemyScore == 1)
        {
            points[2].SetActive(true);
        }
        else if (enemyScore == 2)
        {
            points[3].SetActive(true);
        }
    }

    public void rounds()
    {
        round = playerScore + enemyScore;
        if (round == 1)
        {
            playAudioTrack(3);
        }
        if (round == 2 && playerScore != 2 && enemyScore != 2)
        {
            playAudioTrack(4);
        }
    }
}
