using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotePerson : MonoBehaviour,IInteractable
{
    private GameManager manager;
    [SerializeField,Range(0,10)] private int performance;
    [SerializeField,Range(0,10)] private int salary;
    [SerializeField,Range(0,10)] private int responsibility;
    private void Start()
    {
        manager = GameManager.Instance;
        int stat = performance + salary + responsibility;
        if(stat<=15)
            manager.SetStats(Stats.Bad);
        else if(stat<=25)
            manager.SetStats(Stats.Good);
        else
            manager.SetStats(Stats.Perfect);
    }

    public void Interact()
    {
        manager.SetGameStage(GameStage.Promote);
    }
}
