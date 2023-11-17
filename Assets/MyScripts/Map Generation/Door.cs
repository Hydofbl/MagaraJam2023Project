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

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

        SetDoorStatus(false);
    }

    public void SetDoorStatus(bool status)
    {
        _collider.isTrigger = status;
        IsOpen = status;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Cursor") && IsOpen)
        {
            RoomController.Instance.CurrentRoom = ConnectedRoom;
        }
    }
}
