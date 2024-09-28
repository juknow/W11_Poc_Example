using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackButton()
    {
        SceneManager.LoadScene("Battle");
        GameManager.Instance.EnemyHp = 20;
        GameManager.Instance.EnemyAttack = 5;
    }

}
