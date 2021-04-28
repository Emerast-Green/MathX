using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver()
    {
        print("Hovering2");
    }
    void OnMouseDown()
    {
        Application.Quit();
        print("Clicked2!");
    }
}
