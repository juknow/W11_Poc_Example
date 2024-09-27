using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager _instance;
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update

    //hp variable

    private int playerHp;
    public int PlayerHp
    {
        get { return playerHp; }
        set { PlayerHp = value; }
    }

    private int boyHp;
    public int BoyHp
    {
        get { return boyHp; }
        set { BoyHp = value; }
    }

    private int enemyHp;
    public int EnemyHp
    {
        get { return EnemyHp; }
        set { EnemyHp = value; }
    }


    //attack variable
    private int playerAttack;
    public int PlayerAttack
    {
        get { return playerAttack; }
        set { PlayerAttack = value; }
    }

    private int boyAttack;
    public int BoyAttack
    {
        get { return boyAttack; }
        set { BoyAttack = value; }
    }

    private int enemyAttack;
    public int EnemyAttack
    {
        get { return EnemyAttack; }
        set { EnemyAttack = value; }
    }


    //experince variable
    private int boyBonusStat;
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
