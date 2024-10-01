using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITextController : MonoBehaviour
{
    [Header("HP필드")]
    [SerializeField] private TextMeshProUGUI playerHp;
    [SerializeField] private TextMeshProUGUI boyHp;

    [Header("Attack필드")]
    [SerializeField] private TextMeshProUGUI playerAttack;
    [SerializeField] private TextMeshProUGUI boyAttack;

    [Header("Job필드")]
    [SerializeField] private TextMeshProUGUI playerJob;
    [SerializeField] private TextMeshProUGUI boyJob;

    [Header("Bonus필드")]
    [SerializeField] private TextMeshProUGUI bonusStat;

    [Header("JobSelection필드")]
    [SerializeField] private Canvas firstTeacherJobCanvas;
    [SerializeField] private TMP_Dropdown firstTeacherJobDropDown;
    [SerializeField] private TMP_Dropdown firstBoyJobDropDown;
    [SerializeField] private Button firstJobButton;

    [SerializeField] private TMP_InputField firstTeacherNameInputField;
    [SerializeField] private TMP_InputField firstBoyNameInputField;

    [Header("BattleOver필드")]
    [SerializeField] private Canvas boyJobCanvas;
    [SerializeField] private TMP_Dropdown boyJobDropDown;
    [SerializeField] private Button boyJobButton;
    [SerializeField] private TMP_InputField boyNameInputField;
    [SerializeField] private TextMeshProUGUI explainText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.GameSet == true)
        {
            firstTeacherJobCanvas.gameObject.SetActive(false);
        }
        if (GameManager.Instance.BattleFinish == true)
        {
            boyJobCanvas.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.BattleFinish == false)
        {
            boyJobCanvas.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHp.text = $"{GameManager.Instance.TeacherName} 체력 : " + GameManager.Instance.PlayerHp;
        boyHp.text = $"{GameManager.Instance.BoyName} 체력 : " + GameManager.Instance.BoyHp;
        playerAttack.text = $"{GameManager.Instance.TeacherName} 공격력 : " + GameManager.Instance.PlayerAttack;
        boyAttack.text = $"{GameManager.Instance.BoyName} 공격력 : " + GameManager.Instance.BoyAttack;
        playerJob.text = "내 직업 : " + GetJobName(GameManager.Instance.TeacherJob);
        boyJob.text = "제자 직업 : " + GetJobName(GameManager.Instance.BoyJob);
        explainText.text = "나는 은퇴했습니다." + "\n" + $"{GameManager.Instance.BoyName}는 뒤를 이어 스승이 되었습니다." + "\n" + "당신의 밑으로 새로운 제자가 들어왔습니다.";
    }

    public void FirstSetJob()
    {
        GameManager.Instance.TeacherJob = firstTeacherJobDropDown.value;
        GameManager.Instance.BoyJob = firstBoyJobDropDown.value;
        GameManager.Instance.TeacherColor = Color.red;
        GameManager.Instance.BoyColor = Color.blue;
        GameManager.Instance.GameSet = true;
        GameManager.Instance.TeacherName = firstTeacherNameInputField.text;
        GameManager.Instance.BoyName = firstBoyNameInputField.text;
        firstTeacherJobCanvas.gameObject.SetActive(false);
    }

    private string GetJobName(int jobValue)
    {
        switch (jobValue)
        {
            case 0:
                return "전사";
            case 1:
                return "힐러";
            case 2:
                return "마법사";
            default:
                return "알 수 없음";
        }
    }

    public void SetJob()
    {
        GameManager.Instance.TeacherJob = GameManager.Instance.BoyJob;
        GameManager.Instance.BoyJob = boyJobDropDown.value;
        GameManager.Instance.TeacherColor = GameManager.Instance.BoyColor;
        GameManager.Instance.BoyColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
        GameManager.Instance.TeacherName = GameManager.Instance.BoyName;
        GameManager.Instance.BoyName = boyNameInputField.text;
        GameManager.Instance.BattleFinish = false;
        boyJobCanvas.gameObject.SetActive(false);
    }

}
