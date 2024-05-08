using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ReceptionManager : MonoBehaviour
{
    public static ReceptionManager instance;
    [SerializeField] float receptionTime = 2f;
    [SerializeField] Slider receptionTimerSlider;

    bool readyToRecieveCustomers;
    bool isTimerRunning;
    bool playerInReceptionArea;
    public float timer;

    public event Action TimerCompletedAction;

    private void Awake()
    {
        instance = this;
        readyToRecieveCustomers = true;
        playerInReceptionArea = false;
        isTimerRunning = false;
    }

    private void Start()
    {
        SetupSlider();
        CustomerQueueManager.instance.newCustomerHasComeAction += NewCustomerHasCome;
        RoomManager.instance.someRoomGotVacant += RoomGotVacant;
    }

    private void Update()
    {
        if(readyToRecieveCustomers)
        {   
            if(playerInReceptionArea)
            {
                if(!isTimerRunning)
                {
                    StartTimer();
                }
                
            }

            if(isTimerRunning)
            {
                RunTimer();
            }
        }
    }


    public void StartTimer()
    {
        if(!isTimerRunning)
        {
            receptionTimerSlider.gameObject.SetActive(true);
            SetupSlider();
            timer = receptionTime;
            isTimerRunning = true;
        }
        
    }

    public void RunTimer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            //set reception time value
            float value = receptionTime - timer;
            receptionTimerSlider.value = value;
        }
        else
        {
            Debug.Log("Timer is over");
            isTimerRunning = false;
            timer = receptionTime;
            readyToRecieveCustomers = false;
            TimerCompletedAction?.Invoke();
            receptionTimerSlider.gameObject.SetActive(false);
        }
    }

    public void StopTimer()
    {   
        isTimerRunning = false;
        receptionTimerSlider.value = 0;
    }


    public void SetPlayerInReceptionArea(bool playerInORNot)
    {
        playerInReceptionArea = playerInORNot;
    }

    public void NewCustomerHasCome()
    {
        receptionTimerSlider.gameObject.SetActive(true);
        SetupSlider();
        //check if any free rooms available or not
        Room freeRoom = RoomManager.instance.GetFreeRoom();
        if(freeRoom != null)
        {
            readyToRecieveCustomers = true;
        }
    }

    public void RoomGotVacant()
    {
        if(CustomerQueueManager.instance.GetIfaQueueReady())
        {
            NewCustomerHasCome();
        }
    }

    void SetupSlider()
    {
        receptionTimerSlider.maxValue = receptionTime;
        receptionTimerSlider.value = 0;
    }
}
