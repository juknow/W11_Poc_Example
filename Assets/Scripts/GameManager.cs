using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager _instance;
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update

    //hp variable

    private int playerHp = 30;
    public int PlayerHp
    {
        get { return playerHp; }
        set { PlayerHp = value; }
    }

    private int boyHp = 5;
    public int BoyHp
    {
        get { return boyHp; }
        set { BoyHp = value; }
    }

    private int enemyHp = 10;
    public int EnemyHp
    {
        get { return enemyHp; }
        set { EnemyHp = value; }
    }


    //attack variable
    private int playerAttack = 5;
    public int PlayerAttack
    {
        get { return playerAttack; }
        set { PlayerAttack = value; }
    }

    private int boyAttack = 1;
    public int BoyAttack
    {
        get { return boyAttack; }
        set { BoyAttack = value; }
    }

    private int enemyAttack = 5;
    public int EnemyAttack
    {
        get { return enemyAttack; }
        set { EnemyAttack = value; }
    }


    //experince variable
    private int boyBonusStat = 0;
    public int BoyBonusStat
    {
        get { return boyBonusStat; }
        set { BoyBonusStat = value; }
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
