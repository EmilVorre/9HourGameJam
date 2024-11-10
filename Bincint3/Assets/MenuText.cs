using System;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    float threshold = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("Upate");
        if(UnityEngine.Random.Range(0f, 1f) < this.threshold){
            this.GetComponent<UnityEngine.UI.Image>().enabled = false;
        } else {
            this.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
    }
}
