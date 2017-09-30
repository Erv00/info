using UnityEngine;

public class FlushBox : MonoBehaviour {

    public void Flush()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Destroy>().Kill();
        }
    }
}
