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
    [SerializeField] private GameObject badDecisionText;
    [SerializeField] private GameObject hrTaskCompletedText;
    [SerializeField] private GameObject goodJobText;
    [SerializeField] private GameObject starFallPart;

    private void OnEnable()
    {
        GameEvent.Green += TextGoodShow;
        GameEvent.Red += TextBadShow;
        GameEvent.Panel += ActivePanel;
    }

    private void OnDisable()
    {
        GameEvent.Green -= TextGoodShow;
        GameEvent.Red -= TextBadShow;
        GameEvent.Panel -= ActivePanel;
    }

    private void Start()
    {
        manager = GameManager.Instance;
    }

    private void Update()
    {
        if(manager.buttonTap)
            hrTaskCompletedText.SetActive(true);
    }

    private void ActivePanel()
    {
        StartCoroutine(ActivePanelWait());
    }
    private IEnumerator ActivePanelWait()
    {
        if (manager.GameStage == GameStage.Hiring)
        {
            yield return new WaitForSeconds(1.5f);
            HiringPanel.SetActive(true);
            HiringPanel.transform.DOScale(1.2f, 1f);
        }
        else if (manager.GameStage == GameStage.Promote)
        {
            yield return new WaitForSeconds(1.5f);
            PromotePanel.SetActive(true);
            PromotePanel.transform.DOScale(1.2f, 1f);
        }
    }

    public void GreenButton()
    {
        if(manager.characters.Count == 2)
            manager.buttonTap = true;
        GameEvent.Green();
    }

    public void RedButton()
    {
        if(manager.characters.Count == 2)
            manager.buttonTap = true;
        GameEvent.Red();
    }

    public void TextGoodShow()
    {
        PromotePanel.SetActive(false);
        HiringPanel.SetActive(false);
        if (manager.Stats == Stats.Good && manager.GameStage == GameStage.Hiring)
        {
            starFallPart.SetActive(true);
            TextEdit(correctText);
        }
        else if (manager.Stats == Stats.Bad && manager.GameStage == GameStage.Hiring)
        {
            TextEdit(badDecisionText);
        }
        else if (manager.Stats == Stats.Good && manager.GameStage == GameStage.Promote)
        {
            starFallPart.SetActive(true);
            TextEdit(goodJobText);
        }
        else if (manager.Stats == Stats.Bad && manager.GameStage == GameStage.Promote)
        {
            TextEdit(correctText);
        }
    }
    public void TextBadShow()
    {
        PromotePanel.SetActive(false);
        HiringPanel.SetActive(false);
        if (manager.Stats == Stats.Good && manager.GameStage == GameStage.Hiring)
        {
            TextEdit(badDecisionText);
        }
        else if (manager.Stats == Stats.Bad && manager.GameStage == GameStage.Hiring)
        {
            starFallPart.SetActive(true);
            TextEdit(correctText);
        }
        else if (manager.Stats == Stats.Good && manager.GameStage == GameStage.Promote)
        {
            TextEdit(correctText);
        }
        else if (manager.Stats == Stats.Bad && manager.GameStage == GameStage.Promote)
        {
            starFallPart.SetActive(true);
            TextEdit(goodJobText);
        }
    }

    public void TextEdit(GameObject obj)
    {
        obj.SetActive(true);
        obj.transform.DOScale(1, 2).OnComplete(() => obj.transform.DOScale(0, 1));
    }
}
