using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentscoretoView;
    [SerializeField] GameObject menupannel;

    int _currentScore;
    int count;

    private void Awake()
    {
        menupannel.SetActive(false);
    }

    private void Update()
    {
        //exit
        //TODO - menu to exit
        if (count == 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateMenu();
        }else if (count == 1 && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeLevel();
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
