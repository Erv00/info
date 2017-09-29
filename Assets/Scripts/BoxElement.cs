using UnityEngine;

public class BoxElement {
    private string strVal = "0";

    public BoxElement(string x)
    {
        SetValue(x);
    }

    public BoxElement(int x)
    {
        strVal = x.ToString();
    }

    public void SetValue(string x)
    {
        strVal = x;
    }

    public string GetValue()
    {
        return strVal;
    }

}
