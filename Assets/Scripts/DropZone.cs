using System.Collections;
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
    private Instruction toSet;

    public void OnDrop(PointerEventData data)
    {
        //Preform checking
        if(Type == ZoneType.Delete)
        {
            data.pointerDrag.GetComponent<Draggable>().Delete();
            beingDragged = null;
            isOver = false;
            return;
        }

        if((data.pointerDrag.GetComponent<Instruction>().Type == Instruction.Instructions.JMP && data.pointerDrag.GetComponent<Instruction>().pair ==null) || (data.pointerDrag.GetComponent<Instruction>().Type == Instruction.Instructions.JMPZ && data.pointerDrag.GetComponent<Instruction>().pair == null))
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
        if(data.pointerDrag.GetComponent<Instruction>().Type == Instruction.Instructions.COPYTO || data.pointerDrag.GetComponent<Instruction>().Type == Instruction.Instructions.COPYFROM || data.pointerDrag.GetComponent<Instruction>().Type == Instruction.Instructions.ADD)
        {
            GameObject.Find("Ask").transform.GetChild(0).gameObject.SetActive(true);
            toSet = data.pointerDrag.GetComponent<Instruction>();
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

    public void OnEndEdit(string str)
    {
        try
        {
            toSet.index = System.Int32.Parse(str);
            if (!toSet.GetComponent<Instruction>().wasSet)
            {
                toSet.gameObject.GetComponent<Text>().text += " " + str;
                toSet.GetComponent<Instruction>().wasSet = true;
            }
            else
            {
                string current = toSet.gameObject.GetComponent<Text>().text;
                string baseName = current.Substring(0,current.Length - 2);
                toSet.gameObject.GetComponent<Text>().text = baseName + " " + str;
            }
            GameObject.Find("Question").SetActive(false);
        }
        catch (System.FormatException)
        {
            Debug.LogError("The entert string is not a number");
        }
    }
}
