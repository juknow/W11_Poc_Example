using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EasyBattleButton() {
        SceneManager.LoadScene("Battle");

    }

    public void MediumBattleButton() {
        SceneManager.LoadScene("Battle");

    }

    public void HardBattleButton() {
        SceneManager.LoadScene("Battle");

    }
}
