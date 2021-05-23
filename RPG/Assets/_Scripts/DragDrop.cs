using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private BattleCanvas battle;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 oldPosition;
    private AddMagic addMagic;
    public MagicAvalibility magic;
    private bool droppable = false;
    public bool boosted = false;
    public bool locked = false;
    public GameObject lockAnim;
    private GameObject instantiatedLock;
    private StarSystem stars;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.FindGameObjectWithTag("Battle Canvas").GetComponent<Canvas>();
        battle = GameObject.FindGameObjectWithTag("Battle Canvas").GetComponent<BattleCanvas>();
        stars = GetComponent<StarSystem>();
   //     magic = GameObject.FindGameObjectWithTag("Magic").GetComponent<MagicAvalibility>();
        addMagic = GetComponent<AddMagic>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        oldPosition = eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition;
        droppable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerCurrentRaycast.isValid == false || droppable == false || boosted == true || addMagic.boosted || this.addMagic.orderNumber != eventData.pointerCurrentRaycast.gameObject.GetComponent<AddMagic>().orderNumber)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = oldPosition;
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
       if(eventData.pointerDrag != null)
        {
            Debug.Log(this.addMagic.orderNumber);
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.GetComponent<AddMagic>().orderNumber);
            if (eventData.pointerCurrentRaycast.isValid && boosted == false && eventData.pointerDrag.gameObject.GetComponent<DragDrop>().boosted == false)
            {
                Debug.Log("Valid");
                if(this.addMagic.orderNumber == eventData.pointerDrag.gameObject.GetComponent<AddMagic>().orderNumber)
                {
                Debug.Log("Boost");
                    boosted = true;
                    addMagic.boosted = true;
                    droppable = true;
                    stars.AddStar();
                    magic.DoublePower(0);
                    Destroy(eventData.pointerDrag.gameObject);
                }
            }
            else
                Debug.Log("Not");
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerPress.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void GetLock(GameObject lockGO)
    {
        Vector3 lockPos = transform.Find("LockPos").gameObject.transform.position;
     if(instantiatedLock == null)
        {
           instantiatedLock = Instantiate(lockGO, lockPos, transform.rotation, this.transform);
        }
     else
        {
            instantiatedLock.GetComponent<Animator>().SetBool("isUnlocked", false);
        }
        canvasGroup.alpha = .6f;
    }

    public void Unlock()
    {
        instantiatedLock.GetComponent<Animator>().SetBool("isUnlocked", true);
        canvasGroup.alpha = 1f;
    }
}
