using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Customer : MonoBehaviour
{
    [SerializeField] GameObject[] meshesh;
    [SerializeField] Animator myAnimator;

    private void Start()
    {
        SetRandomMesh();
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
    }

}
