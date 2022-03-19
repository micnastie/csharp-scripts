using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject tospawn;
    public float delay,betterdelay;

    // Start is called before the first frame update
    void Start()
    {
        delay = betterdelay;
    }

    // Update is called once per frame
    void Update()
    {

        delay = delay - Time.deltaTime;
            if(delay <= 0)
        {
            Instantiate(tospawn, transform.position, Quaternion.identity);
            delay = Random.Range(betterdelay-3,betterdelay+3);
        }
    }






}
