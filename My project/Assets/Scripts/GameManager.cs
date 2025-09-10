using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // des game states
    public enum GameState
    {
        MainMenu,
        Gameplay,
        GameOver
    }

    public GameState currentState;

    public GameObject mainMenuPanel;
    public GameObject gameplayPanel;
    public GameObject gameOverPanel;
    public Button firstSelectedButtonMainMenu; // Le bouton "Jouer" par exemple
    public Button firstSelectedButtonGameOver; // Le bouton "Rejouer"

    void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // o cas où
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SwitchState(GameState.MainMenu);
    }

    public void SwitchState(GameState newState)
    {
        currentState = newState;
        StartCoroutine(OnStateEnter(newState));
    }

    private IEnumerator OnStateEnter(GameState state)
    {
        // tou cramer pour repartir sur des bases saines
        mainMenuPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);


        yield return new WaitForEndOfFrame();//sureté

        switch (state)
        {
            case GameState.MainMenu:
                mainMenuPanel.SetActive(true);
                // pour navigation manette
                if (firstSelectedButtonMainMenu != null)
                {
                    firstSelectedButtonMainMenu.Select();
                }
                break;

            case GameState.Gameplay:
                gameplayPanel.SetActive(true);
                // LANCER LA PARTIE
                break;

            case GameState.GameOver:
                gameOverPanel.SetActive(true);
                // pour rejouer
                if (firstSelectedButtonGameOver != null)
                {
                    firstSelectedButtonGameOver.Select();
                }
                break;
        }
    }


    // --- Méthodes publiques appelées par les boutons de l'UI ---

    public void StartGame()
    {
        // Passe à l'état de jeu.
        if (currentState == GameState.MainMenu)
        {
            SwitchState(GameState.Gameplay);
        }
    }

    public void EndGame()
    {
        if (currentState == GameState.Gameplay)
        {
            SwitchState(GameState.GameOver);
        }
    }

    public void ReturnToMainMenu()//Pas forcément nécesssaire en sah
    {
        if (currentState == GameState.GameOver)
        {
            SwitchState(GameState.MainMenu);
        }
    }
}
