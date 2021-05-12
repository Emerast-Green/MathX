using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gamescript : MonoBehaviour
{
    public bool debug;
    public AudioSource win_sound;
    public AudioSource loose_sound;
    public AudioSource background;

    public GameObject[] prefabs;
    public GameObject[] bananas;
    public GameObject[] apples;

    public Text board;
    public Text scoreboard;
    public Text livesboard;
    public Text highboard;
    public Text uselesstext;
    public Text eq_symbol;
    public float background_volume;
    public int stage; //  which stage is active?
    public int level; // what is current level (0-2)

    public Canvas menu;
    public Canvas game;
    public Canvas lvl;

    int itemamount = -1; // whatever is value required 
    int fruit = 0;
    int fruit_ = 0;

    int t; // temporary itemamount value
    int score;
    int lives;
    int highscore;
    string Score;
    string High;
    bool progress;
    bool Continue;
    // Start is called before the first frame update

    void level_Models()
    {
        eq_symbol.text = "WIP";
        itemamount = -1;
        for(int i=0;i<prefabs.Length;i++) 
        {
            bananas[i] = Instantiate(prefabs[i], new Vector3(-8+i*4,10,16), Quaternion.identity);
           // bananas[i].transform.Rotate(-0.595f,0.447f,0.372f,Space.Self);
        };
        if(lives>0){this.Draw(itemamount,fruit,0);};
        progress = false;      
    }
    void Start()
    {
        stage = 0;
        game.planeDistance = -1;
        lvl.planeDistance = -1;
        //this.GameStart();
        uselesstext.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Main Code
        background.volume = background_volume;
        if(background.isPlaying&&debug){background.Stop();};
        if(!background.isPlaying&&!debug){background.Play();};
        if(stage==1){this.GameUpdate();};
    }
    void GameUpdate()  // Code for game's loop
    {
        if(lives==0)
        {
            uselesstext.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                print("Looping game");
                stage = 0;
                game.planeDistance = -1;
                menu.planeDistance = 60;
                uselesstext.gameObject.SetActive(false);
            };

        };
        if(progress==true) // this code is responsible for gameplay itself
        {           
            if(level==0){level_Counting();};
            if(level==1){level_Equation(0);};
            if(level==2){level_Equation(1);};
            if(level==3){level_Models();};
            board.text = "";
            Continue = true;
        };
        if(board.text.Length < itemamount.ToString().Length)
        {
            board.text += Input.inputString;
        }else{
            if(Continue==true)
            {
                if(board.text == itemamount.ToString())
                {
                    win_sound.Play();
                    score+=1;
                    Continue = false;
                    Invoke("Progress",.5f);
                    this.UpdateScore();
                }else{
                    loose_sound.Play();
                    lives-=1;
                    board.text = "";
                    Continue = false;
                    this.Clean();
                    Invoke("Progress",.5f);
                    this.UpdateScore();
                    this.UpdateLives();
                };
            }
        };
    }
    public void level_Counting()
    {
        eq_symbol.text = "";
        t = Random.Range(1,10);
        if(t==itemamount){t=Random.Range(1,10);};
        fruit = Random.Range(0,prefabs.Length);
        itemamount = t;
        print("Random:"+itemamount.ToString());
        this.Clean();
        if(lives>0){this.Draw(itemamount,fruit,0);};
        progress = false;
    }
    public void level_Equation(int tier) // tier = 0 for add,sub & tier = 1 for add,sub,mult
    {
        // Getting Variables for Equation
        int eq = -1;
        if(tier==0){eq=Random.Range(0,2);}else{eq=Random.Range(0,3);};
        int F1 = Random.Range(1,10);
        int F2 = Random.Range(1,10);
        if(eq==2&&F1==10&&F2==10){F1=9;};
        while(F1==F2){F2=Random.Range(1,10);};
        progress = false;
        this.Clean();
        fruit = Random.Range(0,prefabs.Length);
        fruit_ = Random.Range(0,prefabs.Length);
        while(fruit==fruit_){fruit_=Random.Range(0,prefabs.Length);};
        if(lives>0)
        {
            bananas[10]=Instantiate(prefabs[fruit], new Vector3(8.5f,23.55f,16), Quaternion.identity);
            bananas[10].transform.Rotate(0,0,0,Space.Self);
             apples[10]=Instantiate(prefabs[fruit_], new Vector3(15.4f,23.55f,16), Quaternion.identity);
             apples[10].transform.Rotate(0,0,0,Space.Self);
        }
        if(eq==0)
        {
            eq_symbol.text = "+";
            print("Addition "+F1.ToString()+"+"+F2.ToString());
            if(lives>0){this.Draw(F1,fruit,0);};
            if(lives>0){this.Draw(F2,fruit_,1);};
            itemamount = F1+F2;
        };
        if(eq==1)
        {
            eq_symbol.text = "-";
            if(F1<F2){int FT = 0; FT = F1; F1 = F2; F2 = FT;};
            print("Substraction "+F1.ToString()+"-"+F2.ToString());
            if(lives>0){this.Draw(F1,fruit,0);};
            if(lives>0){this.Draw(F2,fruit_,1);};
            itemamount = F1-F2;
        };
        if(eq==2)
        {
            eq_symbol.text = "X";
            print("Multiplication "+F1.ToString()+"x"+F2.ToString());
            if(lives>0){this.Draw(F1,fruit,0);};
            if(lives>0){this.Draw(F2,fruit_,1);};
            itemamount = F1*F2;
        };
    }
    public void GameStart(int l)
    {
        //highscore = PlayerPrefs.GetInt("highscore");
        highscore = PlayerPrefs.GetInt("highscore"+l.ToString());
        lives = 3;
        level = l;
        Continue = true;
        progress = true;
        itemamount = Random.Range(1,10);
        highscore = PlayerPrefs.GetInt("highscore"+l.ToString());
        score = 0;
        Invoke("UpdateHighscore",.1f);
        this.UpdateLives();
    }
    void GameEnd()
    {
        if(score>highscore){PlayerPrefs.SetInt("highscore"+level.ToString(),score);print("NEW HIGHSCORE "+score.ToString());};
        print("death");
        Continue=false;
    }
    void UpdateHighscore()
    {
        //background.volume=background_volume; // It's here 'cuz screw making new function & it's called like once a playthough.
        highboard.text = "Rekord:";
        High = Mathf.Abs(highscore).ToString();
        if(score<0){scoreboard.text+="-";};
        if(High.Length==1){highboard.text+="00"+High;};
        if(High.Length==2){highboard.text+="0"+High;};
        if(High.Length==3){highboard.text+=""+High;};        
    }
    void UpdateScore()
    {
        scoreboard.text = "Wynik:";
        Score = Mathf.Abs(score).ToString();
        if(score<0){scoreboard.text+="-";};
        if(Score.Length==1){scoreboard.text+="00"+Score;};
        if(Score.Length==2){scoreboard.text+="0"+Score;};
        if(Score.Length==3){scoreboard.text+=""+Score;};
    }
    void UpdateLives()
    {
        if(lives==3){livesboard.text="";};
        if(lives==2){livesboard.text="/";};
        if(lives==1){livesboard.text="/      /";};
        if(lives==0){livesboard.text="/      /       /";this.GameEnd();};
    }
    void Progress()
    {
        progress=true;
    }
    void Clean()
    {
        for(int i=0;i<11;i++)
        {
            Destroy(bananas[i],t=0);
            Destroy(apples[i],t=0);
        }
    }
    void Draw(int amount,int prefab_index,int ind)
    {
        // for(int i=0;i<20;i++)
        // {
        //     Destroy(bananas[i]);
        //     bananas[i] = null;
        // }
        for(int i=0;i<amount;i++) 
        {
            if(ind==0)
            {
                bananas[i] = Instantiate(prefabs[prefab_index], new Vector3(-8+i*4,11,16), Quaternion.identity);
                bananas[i].transform.Rotate(0,0,0,Space.Self);
            };
            if(ind==1)
            {
                apples[i] = Instantiate(prefabs[prefab_index], new Vector3(-8+i*4,6,16), Quaternion.identity);
                apples[i].transform.Rotate(0,0,0,Space.Self);
            };            
        };
    }
}
