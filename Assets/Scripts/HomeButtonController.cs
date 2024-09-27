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

    public void EasyBattleButton()
    {
        SceneManager.LoadScene("Battle");
        GameManager.Instance.EnemyHp = 20;
        //GameManager.Instance.EnemyAttack = 5;
    }

    public void MediumBattleButton()
    {
        SceneManager.LoadScene("Battle");
        //GameManager.Instance.EnemyHp = 40;
        //GameManager.Instance.EnemyAttack = 10;

    }

    public void HardBattleButton()
    {
        SceneManager.LoadScene("Battle");
        //GameManager.Instance.EnemyHp = 60;
        //GameManager.Instance.EnemyAttack = 20;

    }
}
