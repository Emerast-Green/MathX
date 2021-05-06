using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backbutton : MonoBehaviour
{
    public gamescript gamescript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        print("Clicked!");
        gamescript.menu.planeDistance = 60;
        gamescript.game.planeDistance = -1;
        gamescript.lvl.planeDistance = -1;
        gamescript.stage = 0;
        //gamescript.GameStart();
    }
}
