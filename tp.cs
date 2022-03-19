using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    // Start is called before the first frame update
    public string message;
    public void onmuseenter()
    {
        tooltipman._instance.setandustp(message);
    }
    public void exit()
    {
        tooltipman._instance.hideandustp();
    }
}
