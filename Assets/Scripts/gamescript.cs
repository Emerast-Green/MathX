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
    public Text board;
    public Text scoreboard;
    public Text livesboard;
    public Text highboard;
    public Text uselesstext;
    public float background_volume;
    public int stage; //  which stage is active?
    public Canvas menu;
    public Canvas game;

    int itemamount = -1;
    int fruit = 0;

    int t; // temporary itemamount value
    int score;
    int lives;
    int highscore;
    string Score;
    string High;
    bool progress;
    bool Continue;
    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
        game.planeDistance = -1;
        this.GameStart();
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
    void GameUpdate()
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
        if(progress==true)
        {           
            t = Random.Range(1,10);
            if(t==itemamount){t=Random.Range(1,10);};
            fruit = Random.Range(0,prefabs.Length);
            itemamount = t;
            print("Random:"+itemamount.ToString());
            if(lives>0){this.Draw(0,itemamount,fruit);}else{this.Clean(itemamount);};
            progress = false;
            board.text = "";
            Continue = true;
        };
        if(board.text.Length < 1)
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
                    Invoke("Progress",.5f);
                    this.UpdateScore();
                    this.UpdateLives();
                };
            }
        };
    }
    public void GameStart()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        lives = 3;
        Continue = true;
        progress = true;
        itemamount = Random.Range(1,10);
        highscore = PlayerPrefs.GetInt("highscore");
        score = 0;
        Invoke("UpdateHighscore",.1f);
        this.UpdateLives();
    }
    void GameEnd()
    {
        if(score>highscore){PlayerPrefs.SetInt("highscore",score);print("NEW HIGHSCORE "+score.ToString());};
        print("death");
        Continue=false;
    }
    void UpdateHighscore()
    {
        //background.volume=background_volume; // It's here 'cuz screw making new function & it's called like once a playthough.
        highboard.text = "Highscore:";
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
        if(lives==3){livesboard.text="♡ ♡ ♡";};
        if(lives==2){livesboard.text="♡ ♡ ☠";};
        if(lives==1){livesboard.text="♡ ☠ ☠";};
        if(lives==0){livesboard.text="☠ ☠ ☠";this.GameEnd();};
    }
    void Progress()
    {
        progress=true;
    }
    void Clean(int amount)
    {
        for(int i=0;i<10;i++)
        {
            Destroy(bananas[i]);
            bananas[i]=null;
        }
    }
    void Draw(int y,int amount,int prefab_index)
    {
        for(int i=0;i<20;i++)
        {
            Destroy(bananas[i]);
            bananas[i] = null;
        }
        for(int i=0;i<amount;i++) 
        {
            bananas[i] = Instantiate(prefabs[prefab_index], new Vector3(-8+i*4,10+y,16), Quaternion.identity);
            bananas[i].transform.Rotate(-84,0,90,Space.Self);
        };
    }
}
