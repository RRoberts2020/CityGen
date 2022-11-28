using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject CameraPreview;
    public GameObject Player;
    public GameObject SaveJSONButton;
    public GameObject SaveFBXButton;
    public GameObject PlayButton;

    public WFCV2_Main WFCFail;

    public WFCV2_Main SuccessCheck;

    public WFCV2_Main SuccessPlayCheck;

    public bool SaveJSONCurrentCity = false;

    public bool LoadJSONCurrentCity = false;

    public bool ExportFBXCurrentCity = false;

    public WFCV2_Main LoadJSONCheck;

    public void Update()
    {
        if (WFCFail.FailCheck == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }

        if (SuccessCheck.SuccessMark == true)
        {

            SaveJSONButton.SetActive(true);
            SaveFBXButton.SetActive(true);
        }
        if (SuccessCheck.SuccessMark == false)
        {

            SaveJSONButton.SetActive(false);
            SaveFBXButton.SetActive(false);
        }

        if (SuccessPlayCheck.SuccessPlayMark || LoadJSONCheck.LoadTimeStop == true)
        {
            PlayButton.SetActive(true);
            
        }
        else
        {
            PlayButton.SetActive(false);
        }
    }

    public void StartLevel()
    {
        Debug.Log("The StartLevel button has been triggered");
        CameraPreview.SetActive(false);
        Player.SetActive(true);
        Canvas.SetActive(false);
    }

    public void SaveFBXLevel()
    {
        Debug.Log("TheSaveFBXLevel button has been triggered");

        ExportFBXCurrentCity = true;
    }

    public void SaveJSONLevel()
    {
        Debug.Log("The SaveJSONLevel button has been triggered");

        SaveJSONCurrentCity = true;
    }

    public void LoadJSONLevel()
    {
        Debug.Log("The LoadLevel button has been triggered");

        LoadJSONCurrentCity = true;
    }

    public void ReloadLevel()
    {
        Debug.Log("The Reload button has been triggered");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("You have quit the game");
            Application.Quit();
        }
    }
}
