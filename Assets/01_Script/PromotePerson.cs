using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotePerson : MonoBehaviour,IInteractable
{
    private GameManager manager;
    private Animator anim;
    [SerializeField] private GameObject angryEmojiPart;
    [SerializeField,Range(0,10)] private int performance;
    [SerializeField,Range(0,10)] private int salary;
    [SerializeField,Range(0,10)] private int responsibility;
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
        int stat = performance + salary + responsibility;
        if(stat<=15)
            manager.SetStats(Stats.Bad);
        else if(stat<=30)
            manager.SetStats(Stats.Good);
    }

    public void Interact()
    {
        manager.SetGameStage(GameStage.Promote);
    }
    private void GreenChoice()
    {
        anim.SetBool("Yes",true);
        StartCoroutine(PlayParticle());
    } 
    private void RedChoice()
    {
        angryEmojiPart.SetActive(true);
        anim.SetBool("No",true);
        StartCoroutine(PlayParticle());
    }

    private IEnumerator PlayParticle()
    {
        yield return new WaitForSeconds(2f);
        if (manager.characters.Count > 2)
        {
            gameObject.SetActive(false);
            manager.characters.Remove(gameObject);
        }
    }
}
