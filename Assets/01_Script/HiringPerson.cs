using System;
using System.Collections;
using UnityEngine;

public class HiringPerson : MonoBehaviour,IInteractable
{
    private GameManager manager;
    private Animator anim;
    [SerializeField] private GameObject starFallPart;
    [SerializeField] private int experience;
    [SerializeField] private int salary;
    [SerializeField] private int references;

    private void OnEnable()
    {
        GameEvent.Correct += AnimatorAndPart;
    }

    private void OnDisable()
    {
        GameEvent.Correct -= AnimatorAndPart;
    }

    private void Start()
    {
        manager = GameManager.Instance;
        anim = GetComponent<Animator>();
        int stat = experience + salary + references;
        if(stat<=15)
            manager.SetStats(Stats.Bad);
        else if(stat<=25)
            manager.SetStats(Stats.Good);
        else
            manager.SetStats(Stats.Perfect);
    }

    public void Interact()
    {
        manager.SetGameStage(GameStage.Hiring);
    }

    public void AnimatorAndPart()
    {
        StartCoroutine(WaitPartAndAnim());
    }

    public IEnumerator WaitPartAndAnim()
    {
        starFallPart.SetActive(true);
        anim.SetBool("Yes",true);
        yield return new WaitForSeconds(2f);
        manager.characters.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
