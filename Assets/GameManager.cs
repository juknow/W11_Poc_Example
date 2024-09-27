using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private static GameManager _instance;
    public static GameManager Instance {get; private set;}
    // Start is called before the first frame update
    private int cropLevel;
    public int CropLevel
    {
        get { return cropLevel; }
        set { cropLevel = value; }
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
