using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countingbutton : MonoBehaviour
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
        gamescript.menu.planeDistance = -1;
        gamescript.game.planeDistance = 60;
        gamescript.lvl.planeDistance = -1;
        gamescript.stage = 1;
        //gamescript.GameStart();
    }
}
