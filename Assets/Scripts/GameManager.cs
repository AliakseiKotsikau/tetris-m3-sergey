using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Board mainBoard;
    [SerializeField]
    private Ghost ghostBoard;
    [Header("UI elements")]
    [SerializeField]
    private GameObject playPanel;
    [SerializeField]
    private TextMeshProUGUI piecesLeftText;

    public UnityAction TetrisModeEnabled;
    public UnityAction Match3ModeEnabled;
    public UnityAction GameStarted;
    public UnityAction GameOver;

    private int piecesLeft = 20;

    public GameState State { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainBoard.PieceLocked += DecreaseNumberOfPieces;
        UpdateNumberOfPieces();
        ghostBoard.enabled = false;
        State = GameState.PAUSE;
    }

    public void OnSwapMode()
    {
        State = State == GameState.TETRIS ? GameState.MATCH_3 : GameState.TETRIS;
        EnableGhostBoardForTetrisMode();

        if (State == GameState.TETRIS)
        {
            TetrisModeEnabled?.Invoke();
        }else if(State == GameState.MATCH_3)
        {
            Match3ModeEnabled?.Invoke();
        }
    }

    public void OnPlay()
    {
        State = GameState.TETRIS;
        EnableGhostBoardForTetrisMode();
        playPanel.gameObject.SetActive(false);

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

    public void DecreaseNumberOfPieces()
    {
        piecesLeft--;
        UpdateNumberOfPieces();
    }

    private void UpdateNumberOfPieces()
    {
        piecesLeftText.text = "Left: " + piecesLeft;
    }
}
