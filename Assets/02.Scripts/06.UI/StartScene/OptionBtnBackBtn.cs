using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBack : MonoBehaviour
{
    public void Button1OnOff()
    {
        DataHolder.Instance.Button1();
    }
    public void Button2OnOff()
    {
        DataHolder.Instance.Button2();
    }
}
