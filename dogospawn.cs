using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class dogospawn : MonoBehaviour
{
    public GameObject thing;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawndog()
    {
        Instantiate(thing, transform.position, Quaternion.identity);
    }
    public void childInstantiateAsChild()
    {

        var createImage = Instantiate(thing) as GameObject;
        createImage.transform.SetParent(canvas.transform, true);
        createImage.transform.localPosition = Vector3.zero;
        


    }
}
