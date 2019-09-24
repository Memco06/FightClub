using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class fixCameraButton : MonoBehaviour
{

    public GameObject cameraButton;
    public GameObject flashButton;
    public GameObject titleBar;
    public GameObject qualityMeter;
    public GameObject playerSlider;
    public GameObject enemySlider;
    private string s = "";
    private bool onOff = false;

    // Start is called before the first frame update
    void Start()
    {
        s = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (DefaultTrackableEventHandler.isCamera)
        {
            cameraButton.SetActive(false);
            flashButton.SetActive(false);
            titleBar.SetActive(false);
            qualityMeter.SetActive(false);
            playerSlider.SetActive(true);
            enemySlider.SetActive(true);
        }
        else
        {
            cameraButton.SetActive(true);
            flashButton.SetActive(true);
            titleBar.SetActive(true);
            qualityMeter.SetActive(true);
            playerSlider.SetActive(false);
            enemySlider.SetActive(false);
        }
    }

    public void refresh()
    {
        SceneManager.LoadScene(s);
    }

    public void toggleFlash()
    {
        if (onOff == false)
        {
            CameraDevice.Instance.SetFlashTorchMode(true);
            onOff = true;
        }
        else
        {
            CameraDevice.Instance.SetFlashTorchMode(false);
            onOff = false;
        }
    }
}
