using System;
using System.Collections;
using UnityEngine;

public class HiringPerson : MonoBehaviour,IInteractable
{
    private GameManager manager;
    private Animator anim;
    [SerializeField] private GameObject sadEmojiPart;
    [SerializeField,Range(0,10)] private int experience;
    [SerializeField,Range(0,10)] private int salary;
    [SerializeField,Range(0,10)] private int references;

    private void OnEnable()
    {
        GameEvent.Green += GreenChoice;
        GameEvent.Red += RedChoice;
    }

    private void OnDisable()
    {
        GameEvent.Green -= GreenChoice;
        GameEvent.Red -= RedChoice;
    }

    private void Start()
    {
        manager = GameManager.Instance;
        anim = GetComponent<Animator>();
        int stat = experience + salary + references;
        if(stat<=15)
            manager.SetStats(Stats.Bad);
        else if(stat<=30)
            manager.SetStats(Stats.Good);
    }

    public void Interact()
    {
        manager.SetGameStage(GameStage.Hiring);
    }

    private void GreenChoice()
    {
        anim.SetBool("Yes",true);
        StartCoroutine(RemoveCha());
    } 
    private void RedChoice()
    {
        sadEmojiPart.SetActive(true);
        anim.SetBool("No",true);
        StartCoroutine(RemoveCha());
    }
    private IEnumerator RemoveCha()
    {
        yield return new WaitForSeconds(2f);
        if(manager.characters.Count > 2)
        {
            gameObject.SetActive(false);
            manager.characters.Remove(gameObject);
        }
    }

}
