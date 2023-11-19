using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left,right, top, bottom
    }

    public DoorType type;
    public Room ConnectedRoom;
    public bool IsOpen;

    public bool HasNextRoom;

    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        HasNextRoom = true;
        SetDoorStatus(false);
    }

    public void SetDoorStatus(bool status)
    {
        if(HasNextRoom)
        {
            _spriteRenderer.enabled = !status;
            _collider.isTrigger = status;
            IsOpen = status;
        }
    }
}
