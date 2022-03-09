using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private GameCanvasController _gameCanvasController;

    [SerializeField]
    private Stage _stage;

    [SerializeField]
    private Cube _cube;

    private enum GameState
    {
        Prepare, Gaming, GameOver, GameClear
    }
    private GameState _currentState = GameState.Prepare;

    // Start is called before the first frame update
    void Start()
    {
        _gameCanvasController.OnClickStartButon.Subscribe(_ => {
            Debug.Log($"Click!!: ");
            switch(_currentState)
            {
                case GameState.Prepare:
                    _stage.StartGame();
                    _gameCanvasController.SetButton(false, "");
                    _gameCanvasController.SetText("");
                    _currentState = GameState.Gaming;
                    break;
                case GameState.Gaming:
                    break;
                case GameState.GameOver:
                    _stage.ResetToStart();
                    _cube.ResetToStart();
                    _cube.Pause(false);
                    _gameCanvasController.SetText("Project MF");
                    _gameCanvasController.SetButton(true, "Start Game");
                    _currentState = GameState.Prepare;
                    break;
                case GameState.GameClear:
                    _stage.ResetToStart();
                    _cube.ResetToStart();
                    _gameCanvasController.SetText("Project MF");
                    _gameCanvasController.SetButton(true, "Start Game");
                    _currentState = GameState.Prepare;
                    break;
            }
        }).AddTo(this);

        _cube.OnGameOver.Subscribe(_ => {
            Debug.Log($"OnGameOver!!: ");
            _stage.StopGame();
            _cube.Pause(true);
            _gameCanvasController.SetText("Game Over");
            _gameCanvasController.SetButton(true, "Reset");
            _currentState = GameState.GameOver;
        }).AddTo(this);

        _cube.OnGameClear.Subscribe(_ => {
            Debug.Log($"OnGameClear!!: ");
            _stage.StopGame();
            _gameCanvasController.SetText("Game Clear!!!");
            _gameCanvasController.SetButton(true, "Reset");
            _currentState = GameState.GameClear;
        }).AddTo(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
