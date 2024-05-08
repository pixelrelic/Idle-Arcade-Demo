using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager instance;
    [SerializeField] Transform collectionPosition;
    [SerializeField] public List<CollectableItem> items;
    [SerializeField] public int itemID = -1;
    public int capacity = 3;
    public event Action carryingSomethingAction;
    public event Action stoppedcarryingAction;


    private void Awake()
    {
        instance = this;
    }


    public int GetItemID()
    {
        return itemID;
    }

    public void AddItemInStack(CollectableItem item)
    {   
        
            if (items.Count < capacity)
            {
                if (item.itemId == itemID)
                {
                    Vector3 positionOffsetVector = new Vector3(0, item.height, 0) * items.Count;
                    items.Add(item);
                    item.transform.SetParent(collectionPosition);
                    item.transform.DOJump(collectionPosition.position + positionOffsetVector, 3, 1, 0.5f).OnComplete(()=>
                    {
                        item.transform.position = collectionPosition.position + positionOffsetVector;
                        item.transform.rotation = collectionPosition.transform.rotation;
                    });
                }
                else if(items.Count == 0)
                {
                    Vector3 positionOffsetVector = new Vector3(0, item.height, 0) * items.Count;
                    items.Add(item);
                    item.transform.SetParent(collectionPosition);
                    item.transform.DOJump(collectionPosition.position + positionOffsetVector, 3, 1, 0.5f).OnComplete(() =>
                    {
                        item.transform.position = collectionPosition.position + positionOffsetVector;
                        item.transform.rotation = collectionPosition.transform.rotation;
                    });
                    itemID = item.itemId;
                    carryingSomethingAction?.Invoke();
                }
            }
            
        
    }


    public void RemoveItemFromStack(int itemId,Transform targetPosition)
    {
        if(itemId == itemID)
        {
            if(items.Count != 0)
            {
                //just remove last item
                int index = items.Count - 1;
                items[index].transform.parent = null;
                items[index].transform.DOJump(targetPosition.position, 3, 1, 0.5f).OnComplete(() =>
                {
                    Destroy(items[index].gameObject);
                    items.RemoveAt(index);
                });
            }

            if (items.Count == 0)
            {
                //resetting item id
                itemID = -1;
                stoppedcarryingAction?.Invoke();
            }
        }
        if (items.Count == 0)
        {
            //resetting item id
            itemID = -1;
            stoppedcarryingAction?.Invoke();
        }
    }

}

