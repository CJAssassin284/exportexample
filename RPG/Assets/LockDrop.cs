using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockDrop : MonoBehaviour, IDropHandler
{
    public GameObject lockGO;
    public DragDrop dragDrop;
    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dragDrop = eventData.pointerDrag.gameObject.GetComponent<DragDrop>();
            if (dragDrop.locked == false)
            {
                dragDrop.GetLock(lockGO);
                dragDrop.locked = true;
            }
            else
            {
                dragDrop.Unlock();
                dragDrop.locked = false;
            }
        }
    }

}
