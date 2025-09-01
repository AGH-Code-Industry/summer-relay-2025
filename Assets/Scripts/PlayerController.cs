using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly List<IInteractable> _interactablesInRange = new();

    const int InventorySize = 5;
    private readonly FixedSizeQueue<IItem> _inventory = new(InventorySize);
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetButtonDown("Interact") && _interactablesInRange.Count > 0)
        {
            var interactable = _interactablesInRange[0];
            interactable.Interact();
            if (!interactable.CanInteract())
            {
                _interactablesInRange.Remove(interactable);
            }
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // Left (-1) or Right (1)
        float moveY = Input.GetAxisRaw("Vertical");   // Down (-1) or Up (1)

        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * movement);

        float clampedX = Mathf.Clamp(transform.position.x, -64f, 64f);
        float clampedY = Mathf.Clamp(transform.position.y, -1.15f, 0.29f);

        transform.position = new Vector3(clampedX, clampedY, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in");
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.CanInteract())
        {
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && _interactablesInRange.Contains(interactable))
        {
            _interactablesInRange.Remove(interactable);
        }
  }
}
