using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popfall : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 popdir;
    public float xmin, xmax;
    public float popF;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float xmod = Random.Range(xmin, xmax);
        popdir.x += xmod;
        rb.AddForce(popdir * popF);
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
