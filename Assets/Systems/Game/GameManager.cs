using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public delegate void GameOverDelegate();

public class GameManager : Singleton<GameManager> 
{
    public GameState m_currentGameState = GameState.Menu;
    public GameObject m_menuHolder;
    public GameObject m_gameHolder;
    public GameObject m_gameOverHolder;
    public GameObject m_youWinHolder;
    public Button m_playButton;
    public Button m_tryAgainButton;
    public Button m_youWinButton;
    public float m_difficultyAmplifier = 0.6f;
    public float m_gameOverTextDelay = 2.0f;

    public AudioSource m_backgroundMusic;
    public AudioClip m_menuMusic;
    public AudioClip m_ingameMusic;
    public GameObject m_gameOverBackground;

    public GameOverDelegate OnGameOver;


    public void Menu()
    {
        m_currentGameState = GameState.Menu;
        SetCurrentHolder(m_menuHolder);
        AudioAnalyzer.Instance.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        m_currentGameState = GameState.Ingame;
        SetCurrentHolder(m_gameHolder);
        m_backgroundMusic.Stop();
        m_backgroundMusic.clip = m_ingameMusic;
        m_backgroundMusic.Play();
        AudioAnalyzer.Instance.Initialize();
    }

    public void YouWin()
    {
        m_currentGameState = GameState.YouWin;
        SetCurrentHolder(m_youWinHolder);
        m_backgroundMusic.Stop();
        m_backgroundMusic.clip = m_menuMusic;
        m_backgroundMusic.loop = true;
        m_backgroundMusic.Play();
        AudioAnalyzer.Instance.Stop();

        // GameOver Stuff
        if (OnGameOver != null)
            OnGameOver.Invoke();
    }

    public void GameOver()
    {
        m_currentGameState = GameState.GameOver;
        AudioAnalyzer.Instance.Stop();

        // Black death background.
        if (m_gameOverBackground != null)
            m_gameOverBackground.SetActive(true);

        // GameOver Stuff
        if (OnGameOver != null)
            OnGameOver.Invoke();

        // Sets Game Over Holder Active after delay.
        StartCoroutine(ShowGameOverWithDelay(m_gameOverTextDelay));
    }

    private IEnumerator ShowGameOverWithDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetCurrentHolder(m_gameOverHolder);
    }

    public void SetCurrentHolder(GameObject current)
    {
        m_menuHolder.SetActive(m_menuHolder == current);
        m_gameHolder.SetActive(m_gameHolder == current);
        m_gameOverHolder.SetActive(m_gameOverHolder == current);
        m_youWinHolder.SetActive(m_youWinHolder == current);
    }

    void Start()
    {
        if (m_gameOverBackground != null)
            m_gameOverBackground.SetActive(false);
    }

    void Update()
    {
        switch (m_currentGameState)
        {
            case GameState.Menu:
                SetUISelection(m_playButton.gameObject);
                break;
            case GameState.Ingame:
                SetUISelection(null);
                break;
            case GameState.GameOver:
                SetUISelection(m_tryAgainButton.gameObject);
                break;
            case GameState.YouWin:
                SetUISelection(m_youWinButton.gameObject);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_currentGameState == GameState.Menu)
            {
                Play();
            }
            else if (m_currentGameState == GameState.YouWin || m_currentGameState == GameState.GameOver)
            {
                Menu();
            }
           
        }
    }

    private void SetUISelection(GameObject target)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(target);
    }
}

public enum GameState
{
    Menu,
    Ingame,
    GameOver,
    YouWin
}