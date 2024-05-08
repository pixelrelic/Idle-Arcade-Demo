using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Room : MonoBehaviour
{
    public bool isOccupied;
    public Customer occupyingCustomer;
    public float stayTime = 5f;
    float timer = 0f;
    public Transform destination;
    bool ifCustomerReachedRoom = false;
    private void Update()
    {
        if(isOccupied)
        {   
            if(ifCustomerReachedRoom)
            {   
                timer -= Time.deltaTime;
                if(timer <=0)
                {
                    //timer is over, make room vavant
                    VacantRoom();
                    RoomManager.instance.RoomGotVacant();
                }
            }
        }
    }


    public void OccupyRoom(Customer customer)
    {
        if(!isOccupied)
        {
            occupyingCustomer = customer;
            isOccupied = true;
            timer = stayTime;
        }
    }

    public void VacantRoom()
    {
        if(isOccupied)
        {
            isOccupied = false;
            //Destroy(occupyingCustomer.gameObject);
            occupyingCustomer.GoToDestination(RoomManager.instance.hotelExit);
            occupyingCustomer = null;
            timer = stayTime;
            ifCustomerReachedRoom = false;
        }
    }

    public void CustomerReachedRoom()
    {
        ifCustomerReachedRoom = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("customer"))
        {
            //if(GameObject.ReferenceEquals(other.gameObject,occupyingCustomer.gameObject))
            {
                Debug.Log("customer reached hotel room");
                occupyingCustomer.StopWalking();
            }
        }
    }
}
