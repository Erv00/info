using UnityEngine;

public class BoxElement {
    private int intVal = 0;
    private string strVal = "0";

    public BoxElement(string x)
    {
        SetValue(x);
    }

    public BoxElement(int x)
    {
        SetValue(x);
    }

    public void SetValue(string x)
    {
        strVal = x[0].ToString();
    }

    public void SetValue(int x)
    {
        intVal = Mathf.Clamp(x,-999,999);
    }

    public int GetValue(int n)
    {
        return intVal;
    }

    public string GetValue()
    {
        return strVal;
    }
}
