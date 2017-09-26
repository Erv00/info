using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler{
    public bool validDrop = false;
    private bool isClone = false;

    #region basi
    // Use this for initialization
    void Start () {
        instance = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    public Transform parentToReturn = null;
    GameObject instance = null;

    public void OnBeginDrag(PointerEventData data)
    {
        #region Create Placeholder
        //Create placeholder
        if (!isClone)
        {
            GameObject tmp = Instantiate(instance);
            tmp.transform.SetParent(transform.parent);
            tmp.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }
        isClone = true;
        #endregion


        parentToReturn = transform.parent;          //Set parent to return to
        transform.SetParent(parentToReturn.parent); //Set parent as parent of parent

        GetComponent<CanvasGroup>().blocksRaycasts = false;     //Enables raycasting
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {

        if (!validDrop)
        {
            Debug.LogWarning("INVALID DROP");
            Delete();
        }
        else
        {
            transform.SetParent(parentToReturn);        //Set parent to previous parent
            GetComponent<CanvasGroup>().blocksRaycasts = true;     //Disables raycasting
            validDrop = false;
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    public void SetClone()
    {
        isClone = true;
    }

}
