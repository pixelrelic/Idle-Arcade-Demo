using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public class Customer : MonoBehaviour
{
    [SerializeField] GameObject[] meshesh;
    [SerializeField] Animator myAnimator;
    [SerializeField] NavMeshAgent myNavMeshAgent;
    bool isGoingToDestination = false;
    bool isGoingToExit = false;
    Transform destination;
    Room currentRoom;

    private void Start()
    {
        
        SetRandomMesh();
    }

    private void Update()
    {
        if (isGoingToDestination)
        {

            if ((int)Vector3.Distance(transform.position, destination.position) <= 0)
            {
                StopWalking();
                isGoingToDestination = false;
                Debug.Log("Customer reached in room");
                currentRoom.CustomerReachedRoom();
            }


        }

        if (isGoingToExit)
        {

            if ((int)Vector3.Distance(transform.position, destination.position) <= 0)
            {
                StopWalking();
                isGoingToExit = false;
                Destroy(gameObject);
            }


        }
    }


    void SetRandomMesh()
    {
        int meshid = Random.Range(0, meshesh.Length);
        foreach(GameObject mesh in meshesh)
        {
            mesh.SetActive(false);
        }

        meshesh[meshid].SetActive(true);

        myAnimator = meshesh[meshid].GetComponent<Animator>();
    }

    public void StartWalking()
    {
        myAnimator.SetBool("walking", true);
    }

    public void StopWalking()
    {
        myAnimator.SetBool("walking", false);
    }

   
    public void GoToRoom(Room room)
    {   
        Debug.Log("Customer going to room");
        currentRoom = room;
        myNavMeshAgent.SetDestination(room.destination.position);
        destination = room.destination;
        StartWalking();
        isGoingToDestination = true;
    }

    public void GoToDestination(GameObject targetDestination)
    {
        myNavMeshAgent.SetDestination(targetDestination.transform.position);
        destination = targetDestination.transform;
        StartWalking();
        isGoingToExit = true;
    }
}
