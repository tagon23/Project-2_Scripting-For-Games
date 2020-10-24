using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentscoretoView;
    [SerializeField] GameObject menupannel;
    [SerializeField] GameObject player;
    [SerializeField] GameObject HUD;
    public Button Resume;

    int _currentScore;
    int count;

    private void Awake()
    {
        menupannel.SetActive(false);
        player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    private void Start()
    {
        Resume.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ResumeLevel();
        player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("button pushed. Have a nice day.");
    }

    private void Update()
    {
        //exit
        //TODO - menu to exit
        if (count == 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateMenu();
            player.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else if (count == 1 && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeLevel();
            player.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }





        //increase score
        //TODO replace with real implementation
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
    }

    public void ExitLevel()
    {
        //compare score to high score
        int highScore = PlayerPrefs.GetInt("High Score");
        if(_currentScore > highScore)
        {
            //save current score as new high score
            PlayerPrefs.SetInt("High Score", _currentScore);
            Debug.Log("New High Score: " + _currentScore);
        }

        //loads main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increasing score
        _currentScore += scoreIncrease;
        //Update Score display
        _currentscoretoView.text = "Score: " + _currentScore.ToString();
    }

    public void ActivateMenu()
    {
        menupannel.SetActive(true);
        Debug.Log("banana");
        count += 1;
    }

    public void ResumeLevel()
    {
        menupannel.SetActive(false);
        count = 0;
    }

    
    
}
