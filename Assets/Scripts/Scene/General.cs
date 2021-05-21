using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class General : MonoBehaviour
{
    [SerializeField] private GameObject _startGamePanel;
    [SerializeField] private GameObject _endGamePanel;

    private int _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 0;
        _startGamePanel.SetActive(true);
        _endGamePanel.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Block>().IsStay)
        {
            Time.timeScale = 0;
            _endGamePanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(_currentScene);
    }

    public void CloseStartGamePanel()
    {
        Time.timeScale = 1;
        _startGamePanel.SetActive(false);
    }
}
