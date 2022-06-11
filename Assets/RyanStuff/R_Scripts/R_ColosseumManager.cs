using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_ColosseumManager : MonoBehaviour
{
    Transform player;

    public R_Enemy tClub;
    public List<R_Enemy> wave;         //the wave of enemies
    public Transform[] spawnPoints;//the spawnpoints
    int waveNum;            //current wave number
    int enemiesPerWave;     //number of enemies there will be in a wave
    int enemiesRemaining;   //number of remaining enemies in a wave

    public Text startText;  //the text the displays the initial countdown to game start
    public Text waveText;   //the text that displays the current wave
    public Text enemyText;  //the text that displays the number of enemies remaining
    float startMatchTimer;


    private hpScript HP;

    // Start is called before the first frame update
    void Start()
    {
        startMatchTimer = 3.0f;
        waveNum = 0;
        wave = new List<R_Enemy>();
        enemiesRemaining = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // Update is called once per frame
    void Update()
    {

        waveText.text = "Wave: " + waveNum;
        startText.text = "Match starts in: " + Mathf.FloorToInt(startMatchTimer);
        enemyText.text = "# of Enemies: " + enemiesRemaining;
        startMatchTimer -= Time.deltaTime;
        if (startMatchTimer < 0)
        {
            startText.text = "Game has started!";

        }

        enemiesRemaining = wave.Count; //update enemies remaining
        SetStateOfWave();              //update the enemies (check if one is too close)

        if (startMatchTimer < 0)
        {
            
            waveNum += 1;
            CalculateEnemiesPerWave();
            CreateWave();

            startMatchTimer = 20;
        }


    }

    void CreateWave()
    {
        Random rand = new Random();

        for (int i = 0; i < enemiesPerWave; i++)
        {
            int randomNum = Random.Range(0, 4);
            R_Enemy newDude = Instantiate(tClub, spawnPoints[randomNum].position, spawnPoints[randomNum].rotation);
            wave.Add(newDude);
        }
    }

    //make the wave aggro the player
    //the if statement in the middle can be removed when the player can actually kill enemies
    void SetStateOfWave()
    {
       // bool lostDude = false;
        if (enemiesRemaining > 0)
        {

            for (int i = 0; i < enemiesRemaining; i++)
            {
                wave[i].InfChase(); //aggro the player forever

                HP = wave[i].gameObject.GetComponent<hpScript>();
                //**THIS NEEDS TO BE ADRESSED FOR THE SCRIPT TO WORK!!!
                //change this to if (wave[i].hp == 0) then remove
                if (HP.hp == 0)
                {
                    //may want to switch to destroy in future, if it works
                    wave.RemoveAt(i);

                }
            }
        }
    }

    //calculate the number of enemies for the wave
    int CalculateEnemiesPerWave()
    {
        enemiesPerWave = waveNum * 3;
        return (waveNum * 3);
    }
}
