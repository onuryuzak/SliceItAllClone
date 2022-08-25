using System.Collections;
using System.Collections.Generic;
using Onur.Template;
using UnityEngine;
using DG.Tweening;

public class GameManager : BaseSingleton<GameManager>
{
    public FloatingText FloatingText;

    [SerializeField] private Material[] PartMaterials;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] LevelGenerator _levelGenerator;
    [SerializeField] private float _delayBeforeGameEnd = .75f;
    [SerializeField] GameObject _confetti;

    [HideInInspector] public GameState gameState;
    [HideInInspector] public int currentMoneyValue;
    public bool canPlay;

    #region BASE

    void Start()
    {
        _confetti.SetActive(false);
        initialize();
        canPlay = false;
        gameState = GameState.TapToPlay;
    }

    #endregion

    #region INGAMEMETHODS

    public Material GetRandomMaterial()
    {
        int randomNumber = Random.Range(0, PartMaterials.Length - 1);
        return PartMaterials[randomNumber];
    }

    public void StartLevel()
    {
        gameState = GameState.Play;
    }

    public void SetGameState(GameState nextState, float delay = 0)
    {
        if (gameState == GameState.End) return;
        if (gameState == nextState) return;
        if (nextState == GameState.Success)
        {
            gameState = GameState.End;
            _confetti.SetActive(true);
            EventManager.OnLevelSuccess();
        }
        else if (nextState == GameState.Fail)
        {
            gameState = GameState.End;
            Time.timeScale = 0;
            EventManager.OnLevelFailed();
        }

        gameState = nextState;
    }

    public void Fail()
    {
        SetGameState(GameState.Fail, _delayBeforeGameEnd);
        canPlay = false;
    }

    public void Success()
    {
        canPlay = false;
        SetGameState(GameState.Success, _delayBeforeGameEnd);
    }

    private void OnApplicationQuit() => DOTween.KillAll();

    #endregion

    #region LEVELMETHODS

    void initialize()
    {
        _levelGenerator.initialize();
        _levelManager.initialize();

        LevelPrefabSO levelSO = _levelManager.currentLevel;
        LoadLevel(levelSO);
    }

    void LoadLevel(LevelPrefabSO levelSO)
    {
        Time.timeScale = 1;
        Level level = _levelGenerator.loadLevel(levelSO);
        EventManager.OnLevelLoaded();
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        _confetti.SetActive(false);
        LevelPrefabSO nextLevel = _levelManager.nextLevel();
        LoadLevel(nextLevel);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1;
        LevelPrefabSO currentLevel = _levelManager.currentLevel;
        LoadLevel(currentLevel);
    }

    #endregion
}