using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PlayerCollisionHandler : MonoBehaviour
{
    CameraSwitcher myCameraSwitcher;

    bool dropItem = false;
    // Start is called before the first frame update
    void Start()
    {
        myCameraSwitcher = GetComponent<CameraSwitcher>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("room"))
        {
            myCameraSwitcher.ActiavteLeftCam();
        }

        if (other.gameObject.CompareTag("room left"))
        {
            myCameraSwitcher.ActiavteRightCam();
        }

        if (other.gameObject.CompareTag("Reception area"))
        {
            Debug.Log("Player entered reception area");
            ReceptionManager.instance.StartTimer();
            ReceptionManager.instance.SetPlayerInReceptionArea(true);
        }

        if (other.gameObject.CompareTag("cash"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.DOJump(transform.position, 3, 1, 0.5f).OnComplete(()=>{
                //todo : hardcoded money amount here
                MoneyManager.instance.CollectMoney(100);
                Destroy(other.gameObject);
            } );
        }

        if (other.gameObject.CompareTag("collectable item"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            CollectableItem item = other.gameObject.GetComponent<CollectableItem>();
            CollectableManager.instance.AddItemInStack(item);
        }
        if (other.gameObject.CompareTag("drop zone"))
        {
            dropItem = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("drop zone"))
        {
            DropZone zone = other.gameObject.GetComponent<DropZone>();
            StartCoroutine(DropItem(zone));
        }
    }

    IEnumerator DropItem(DropZone zone)
    {   
        //while(dropItem)
        {
            
            if (zone.itemid == CollectableManager.instance.GetItemID())
            {
                CollectableManager.instance.RemoveItemFromStack(zone.itemid, zone.transform);
                yield return new WaitForSeconds(1f);
                if(CollectableManager.instance.items.Count == 0)
                {
                    dropItem = false;
                }
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("room"))
        {
            myCameraSwitcher.ActiavteMainCam();
        }

        if (other.gameObject.CompareTag("room left"))
        {
            myCameraSwitcher.ActiavteMainCam();
        }

        if (other.gameObject.CompareTag("Reception area"))
        {
            Debug.Log("Player got out of reception area");
            ReceptionManager.instance.StopTimer();
            ReceptionManager.instance.SetPlayerInReceptionArea(false);

        }

        if (other.gameObject.CompareTag("drop zone"))
        {
            dropItem = false;
        }
    }


}
