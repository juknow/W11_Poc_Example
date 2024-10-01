using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager _instance;
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update


    //battle variable
    private bool battleFinish = false;
    public bool BattleFinish
    {
        get { return battleFinish; }
        set { battleFinish = value; }
    }

    // job variable
    private int teacherJob = 0;
    public int TeacherJob
    {
        get { return teacherJob; }
        set { teacherJob = value; }
    }
    private int boyJob = 0;
    public int BoyJob
    {
        get { return boyJob; }
        set { boyJob = value; }
    }


    //hp variable

    private int playerHp = 50;
    public int PlayerHp
    {
        get { return playerHp; }
        set { playerHp = value; }
    }

    private int boyHp = 30;
    public int BoyHp
    {
        get { return boyHp; }
        set { boyHp = value; }
    }

    private int enemyHp = 50;
    public int EnemyHp
    {
        get { return enemyHp; }
        set { enemyHp = value; }
    }


    //attack variable
    private int playerAttack = 10;
    public int PlayerAttack
    {
        get { return playerAttack; }
        set { playerAttack = value; }
    }

    private int boyAttack = 5;
    public int BoyAttack
    {
        get { return boyAttack; }
        set { boyAttack = value; }
    }

    private int enemyAttack = 5;
    public int EnemyAttack
    {
        get { return enemyAttack; }
        set { enemyAttack = value; }
    }


    //experince variable
    private int boyBonusStat = 5;
    public int BoyBonusStat
    {
        get { return boyBonusStat; }
        set { boyBonusStat = value; }
    }





    /*
    private void Awake() {
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    */

    void Awake()
    {
        // singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
