using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CustomerQueueManager : MonoBehaviour
{
    public static CustomerQueueManager instance;
    [SerializeField] GameObject[] positions;
    [SerializeField] List<Customer> customers;
    [SerializeField] GameObject customerGameobject;
    [SerializeField] GameObject queueParent;
    [SerializeField] GameObject personSpawnPosition;
    bool isQueueReady = true;
    public event Action newCustomerHasComeAction;
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        ReceptionManager.instance.TimerCompletedAction += RemoveFirstCustomer;
        InitiateQueue();

    }
    

    void InitiateQueue()
    {
        foreach(GameObject position in positions)
        {
           GameObject tempGO = Instantiate(customerGameobject, position.transform.position, position.transform.rotation, queueParent.transform);
            Customer tempCustomer = tempGO.GetComponent<Customer>();
            customers.Add(tempCustomer);
        }
    }

    public bool CheckIfCustomerIsInWaiting()
    {
        if(customers[0] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Customer GetFirstCustomer()
    {
        return customers[0];
    }

    public void RemoveFirstCustomer()
    {
        RoomManager.instance.BookRoom(customers[0]);
        isQueueReady = false;
        GameObject tempGO = Instantiate(customerGameobject, personSpawnPosition.transform.position, personSpawnPosition.transform.rotation, queueParent.transform);
        Customer tempCustomer = tempGO.GetComponent<Customer>();
        customers.Add(tempCustomer);

        int lastIndex = customers.Count - 1;

        for(int i = lastIndex ; i > 0 ; i-- )
        {
            GameObject currentCustomer = customers[i].gameObject;
            if(i == 1)
            {
                currentCustomer.transform.DOMove(customers[i - 1].transform.position, 0.5f).OnComplete(()=> {
                    
                    //book room for first customer
                    //RoomManager.instance.BookRoom(customers[0]);
                    customers.RemoveAt(0);
                    newCustomerHasComeAction?.Invoke();
                    isQueueReady = true;
                });
            }
            else
            {
                currentCustomer.transform.DOMove(customers[i - 1].transform.position, 0.5f);
            }
            
        }

    }

    public bool GetIfaQueueReady()
    {
        return isQueueReady;
    }
}
