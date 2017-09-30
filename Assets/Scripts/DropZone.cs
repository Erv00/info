using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler {

#region basic
    // Use this for initialization
    void Start () {
        jmpHolder = GameObject.Find("JMPH");
	}
	
	// Update is called once per frame
	void Update () {
        if (isOver)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform tmpChild = transform.GetChild(i);
                if(beingDragged.transform.position.y > tmpChild.position.y)
                {
                    tmp.transform.SetSiblingIndex(tmpChild.GetSiblingIndex());
                    break;
                }
            }
        }
	}
    #endregion

    public enum ZoneType {Delete,Normal}
    [SerializeField]
    private ZoneType Type = ZoneType.Normal;
    [SerializeField]
    private bool doReorder = false;
    private GameObject tmp = null;
    [SerializeField]
    private bool isOver = false;
    [SerializeField]
    private GameObject beingDragged = null;
    [SerializeField]
    private int jmpCounter = 0;
    [SerializeField]
    private GameObject jmpHolder;

    public void OnDrop(PointerEventData data)
    {
        //Preform checking
        if(Type == ZoneType.Delete)
        {
            Destroy(data.pointerDrag.gameObject);
            beingDragged = null;
            isOver = false;
            return;
        }

        if(data.pointerDrag.GetComponent<Instruction>().Type == Instruction.Instructions.JMP)
        {
            GameObject jmpH = Instantiate(jmpHolder);
            jmpH.GetComponent<Text>().text = jmpCounter.ToString();
            data.pointerDrag.GetComponent<Text>().text += jmpCounter.ToString();
            jmpH.GetComponent<Draggable>().SetClone();
            jmpH.GetComponent<Instruction>().pair = data.pointerDrag;
            data.pointerDrag.GetComponent<Instruction>().pair = jmpH;
            jmpCounter++;
            jmpH.transform.SetParent(transform);
            jmpH.transform.SetSiblingIndex(tmp.transform.GetSiblingIndex());
        }
        
        data.pointerDrag.GetComponent<Draggable>().validDrop = true;
        data.pointerDrag.GetComponent<Draggable>().parentToReturn = transform;
        beingDragged.GetComponent<Draggable>().SetSib(tmp.transform.GetSiblingIndex());
        beingDragged = null;
        isOver = false;
        Destroy(tmp);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        beingDragged = eventData.pointerDrag;
        if (doReorder)
        {
            #region create placeholder      
            if (eventData.dragging)
            {
                isOver = true;
                tmp = new GameObject();
                tmp.transform.SetParent(transform);
                LayoutElement le = tmp.AddComponent<LayoutElement>();
                le.preferredWidth = 160;
                le.preferredHeight = 38.2f;
                le.flexibleWidth = 0;
                le.flexibleHeight = 0;
                tmp.transform.SetParent(transform);
            }
            #endregion
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        beingDragged = null;
        if(doReorder && eventData.dragging)
        {
            Destroy(tmp);
        }
    }
}
