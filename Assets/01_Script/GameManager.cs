using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler<OnGameStageChangedEventArgs> OnGameStageChanged;
    public event EventHandler<OnStatsChangedEventArgs> OnStatsStageChanged;
    
    public GameStage GameStage { get; private set; }
    public Stats Stats { get; private set; }
    public List<GameObject> characters = new List<GameObject>();
    public GameObject poof;

    private void Start()
    {
        SetGameStage(GameStage.Empty);
    }

    private void Update()
    {
        if (characters == null) return;
        characters[0].SetActive(true);

    }

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;

        OnGameStageChanged?.Invoke(this, new OnGameStageChangedEventArgs { gameStage = gameStage });

    }
    public void SetStats(Stats stats)
    {
        Stats = stats;

        OnStatsStageChanged?.Invoke(this, new OnStatsChangedEventArgs { stats = stats });

    }
    public class OnGameStageChangedEventArgs : EventArgs
    {
        public GameStage gameStage;
    }
    public class OnStatsChangedEventArgs : EventArgs
    {
        public Stats stats;
    }

}

public enum GameStage { Empty,Hiring, Promote }
public enum Stats{Bad,Good}
