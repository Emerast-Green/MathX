using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamebutton : MonoBehaviour
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
    void OnMouseOver()
    {
        print("Hovering");
    }
    void OnMouseDown()
    {
        print("Clicked!");
        gamescript.menu.planeDistance = -1;
        gamescript.game.planeDistance = -1;
        gamescript.lvl.planeDistance = 60;
        gamescript.stage = 2;
        //gamescript.GameStart();
    }
}
