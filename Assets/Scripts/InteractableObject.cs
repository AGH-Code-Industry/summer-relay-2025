using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacting!");
    }

    public bool CanInteract() => true;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
