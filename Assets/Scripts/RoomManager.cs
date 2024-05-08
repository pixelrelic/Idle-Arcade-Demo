using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    [SerializeField] Room[] rooms;
    public event Action someRoomGotVacant;
    public GameObject hotelExit;
    private void Awake()
    {
        instance = this;
    }

    public Room GetFreeRoom()
    {
        foreach(Room room in rooms)
        {
            if(!room.isOccupied)
            {
                return room;
            }
        }

        return null;
    }

    public bool BookRoom(Customer customer)
    {
        foreach (Room room in rooms)
        {
            if (!room.isOccupied)
            {
                room.OccupyRoom(customer);
                Debug.Log("Room booked");
                customer.GoToRoom(room);
                return true;              
            }
        }

        return false;
    }

    public void RoomGotVacant()
    {
        someRoomGotVacant?.Invoke();
        Debug.Log("Room got free");
    }

}
