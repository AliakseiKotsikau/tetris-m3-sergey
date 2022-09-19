using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Ghost ghostBoard;
    [Header("UI elements")]
    [SerializeField]
    private GameObject playPanel;

    public UnityAction ModeSwaped;
    public UnityAction GameStarted;

    public GameState State { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ghostBoard.enabled = false;
        State = GameState.PAUSE;
    }

    public void OnSwapMode()
    {
        State = State == GameState.TETRIS ? GameState.MATCH_3 : GameState.TETRIS;
        EnableGhostBoardForTetrisMode();

        ModeSwaped?.Invoke();
    }

    public void OnPlay()
    {
        EnableGhostBoardForTetrisMode();
        playPanel.gameObject.SetActive(false);
        State = GameState.TETRIS;

        GameStarted?.Invoke();
    }

    private void EnableGhostBoardForTetrisMode()
    {
        ghostBoard.enabled = GameState.TETRIS == State;
    }

    public bool IsMatch3Mode()
    {
        return GameState.MATCH_3 == State;
    }
    
    public bool IsTetrisMode()
    {
        return GameState.TETRIS == State;
    }
}
