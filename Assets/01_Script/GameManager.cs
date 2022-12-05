using System;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler<OnGameStageChangedEventArgs> OnGameStageChanged;
    
    public GameStage GameStage { get; private set; }

    private void Start()
    {
        SetGameStage(GameStage.Loaded);
    }

    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;

        OnGameStageChanged?.Invoke(this, new OnGameStageChangedEventArgs { gameStage = gameStage });

    }

    public class OnGameStageChangedEventArgs : EventArgs
    {
        public GameStage gameStage;
    }


}

public enum GameStage { NotLoaded, Loaded, Started, Win, Fail }
