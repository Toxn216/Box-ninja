using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;// добавляем кнопки
    private GameManager gameManager;// добавляем гейм менеджер в скрипт
    public int difficulty;// выбор уровня сложности
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();// добавляем кнопки
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();// добавляем гейм менеджер
        button.onClick.AddListener(SetDifficulty);// вечное ожидание нажатия кнопки
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked");// просто показывает какой уровень мы выбрали в логах
        gameManager.StartGame(difficulty);//передаем значение уровня сложности в метод старт гейм
    }
}
