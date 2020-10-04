using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    [SerializeField] Text _highScoreTextView;



    // Start is called before the first frame update
    void Start()
    {
        //load high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();

        if(_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("please quit");
    }

    
}
