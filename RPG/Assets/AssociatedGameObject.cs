using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssociatedGameObject : MonoBehaviour
{
    public GameObject associatedGameObject;
    public DragDrop dragDrop;

    public void Awake()
    {
        dragDrop = associatedGameObject.GetComponent<DragDrop>();
    }
}
