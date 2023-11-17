using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private float cursorHealth;
    [SerializeField] private float cursorDamage;
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Door>(out Door door))
        {
            if (other.CompareTag("Door") && door.IsOpen)
            {
                RoomController.Instance.CurrentRoom = door.ConnectedRoom;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy Enemy))
        {
            Enemy.GetHit(cursorDamage);
        }
    }
}
