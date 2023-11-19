using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private CursorData cursorData;

    public int CursorHealth;
    public int CursorDamage;

    public static CursorManager Instance;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        CursorDamage = cursorData.Damage;
        CursorHealth = cursorData.Health;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            if (other.TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen && door.HasNextRoom)
                {
                    DungeonController.Instance.CurrentRoom = door.ConnectedRoom;
                }
            }
        }
    }
}
