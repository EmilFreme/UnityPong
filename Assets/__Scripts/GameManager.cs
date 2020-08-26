using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int pointsP1, pointsP2;

    [SerializeField]
    TextMeshProUGUI pointsP1GUI, pointsP2GUI;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject endPanel;

    public enum GameState { Menu, Playing, Paused, Endgame}

    public GameState gameState;
    
    // Start is called before the first frame update
    void Start()
    {
        pointsP1 = 0;
        pointsP2 = 0;

        gameState = GameState.Menu;

        pausePanel.SetActive(false);

        pointsP1GUI.text = "0";
        pointsP2GUI.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PointMade(int player)
    {
        switch (player)
        {
            case 1:
                pointsP1++;
                break;
            case 2:
                pointsP2++;
                break;
        }
        pointsP1GUI.text = pointsP1.ToString();
        pointsP2GUI.text = pointsP2.ToString();

        if(pointsP1 > 10 || pointsP2 > 10)
        {
            StartCoroutine("EndGame");
        }
    }

    private void PauseGame()
    {
        if (gameState == GameState.Playing)
        {
            Time.timeScale = 0;
            gameState = GameState.Paused;
        }
        else if(gameState == GameState.Paused)
        {
            Time.timeScale = 1;
            gameState = GameState.Playing;
        }
        pausePanel.SetActive(gameState == GameState.Paused);
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        gameState = GameState.Playing;
        pointsP1 = 0;
        pointsP2 = 0;
    }

    IEnumerator EndGame()
    {
        gameState = GameState.Endgame;
        endPanel.SetActive(true);

        TextMeshProUGUI endtext = endPanel.GetComponentInChildren<TextMeshProUGUI>();

        endtext.text = pointsP1 > 10 ? "Player 1 Wins" : "Player 2 Wins";

        yield return new WaitForSeconds(3);

        endPanel.SetActive(false);
        menuPanel.SetActive(true);
        gameState = GameState.Menu;
    

    }
}
