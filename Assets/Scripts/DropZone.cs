using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour,IDropHandler {

#region basic
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    public enum ZoneType {Delete,Normal}
    [SerializeField]
    private ZoneType Type = ZoneType.Normal;

    public void OnDrop(PointerEventData data)
    {
        //Preform checking
        if(Type == ZoneType.Delete)
        {
            Destroy(data.pointerDrag.gameObject);
            return;
        }

        data.pointerDrag.GetComponent<Draggable>().validDrop = true;
        data.pointerDrag.GetComponent<Draggable>().parentToReturn = transform;
    }

}
