using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudmove : MonoBehaviour
{
    public float speed;
    public bool left;
    public SpriteRenderer sr;
    public float destroyTime;
    public Sprite[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-5f, 5f), 0);
        speed = Random.Range(.1f, .5f) + speed;
        sr = GetComponent<SpriteRenderer>();
        float scaler = Random.Range(.75f, 1.25f);
        transform.localScale = new Vector3(scaler,scaler,1);
        Color rcolor = sr.color;//created local color
        rcolor.a = Random.Range(.1f,.5f);//modified the apha of local color
        sr.color = rcolor;
        sr.sprite = clouds[Random.Range(0, clouds.Length )];
        if(left)
        {
          speed=  speed * -1;
        }
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*speed);
    }
}
