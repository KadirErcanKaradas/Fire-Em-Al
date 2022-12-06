using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    var interactable = other.GetComponent<IInteractable>();
    if (interactable != null)
    {
      interactable.Interact();
      GameEvent.Panel();
    }

  }
}
