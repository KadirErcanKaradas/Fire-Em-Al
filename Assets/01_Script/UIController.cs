using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] private GameObject HiringPanel;
    [SerializeField] private GameObject PromotePanel;
    [SerializeField] private GameObject correctText;

    private void OnEnable()
    {
        GameEvent.Correct += CorrectShow;
    }

    private void OnDisable()
    {
        GameEvent.Correct -= CorrectShow;
    }

    private void Start()
    {
        manager = GameManager.Instance;
    }

    private void Update()
    {
        StartCoroutine(ActivePanel());
    }

    private IEnumerator ActivePanel()
    {
        if (manager.GameStage == GameStage.Hiring)
        {
            yield return new WaitForSeconds(1.5f);
            HiringPanel.SetActive(true);
            HiringPanel.transform.DOScale(1.2f, 1f);
            manager.SetGameStage(GameStage.Empty);
        }
        else if (manager.GameStage == GameStage.Promote)
        {
            yield return new WaitForSeconds(1.5f);
            PromotePanel.SetActive(true);
            PromotePanel.transform.DOScale(1.2f, 1f);
            manager.SetGameStage(GameStage.Empty);
        }
    }

    public void GreenButton()
    {
        if (manager.Stats == Stats.Bad)
            GameEvent.Wrong();
        else
            GameEvent.Correct();
    }

    public void RedButton()
    {
        if (manager.Stats == Stats.Bad)
            GameEvent.Correct();
        else
            GameEvent.Wrong();
    }

    public void CorrectShow()
    {
        PromotePanel.SetActive(false);
        HiringPanel.SetActive(false);

        correctText.SetActive(true);
        correctText.transform.DOScale(1, 2).OnComplete(() => correctText.transform.DOScale(0, 1));
    }
}
