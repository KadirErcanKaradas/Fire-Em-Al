using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler<OnGameStageChangedEventArgs> OnGameStageChanged;
    public event EventHandler<OnStatsChangedEventArgs> OnStatsStageChanged;
    
    public GameStage GameStage { get; private set; }
    public Stats Stats { get; private set; }
    public List<GameObject> characters = new List<GameObject>();
    private Camera cam;
    public bool buttonTap;

    private void Start()
    {
        cam = Camera.main;
        SetGameStage(GameStage.Empty);
    }

    private void Update()
    {
        if (characters.Count == 0) return;
        characters[0].SetActive(true);

        if (characters.Count == 2 && buttonTap)
        {
            StartCoroutine(LoadScene());
        }

    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < cam.gameObject.transform.childCount; i++)
        {
            cam.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
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
