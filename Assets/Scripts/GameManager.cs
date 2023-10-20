using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;//подрубаем текст с очками
    public TextMeshProUGUI gameOverText; //подрубаем текст с гейм овером
    public TextMeshProUGUI livesText; //Текст с жизнями   
    public Button restartButton;// подрубаем кнопку рестарта
    public GameObject titleScreen;// подрубаем татйтл скрин
    public GameObject pauseMenuUI;//Подрубаем пауз меню
    public AudioSource mainMusic;//подруб обьекта музыки из иерархии
    public bool playMusic;//подруб флага музыки
    public bool isGameActive;//продолжается ли игра
    public static bool isGamePaused = false;// по дэфолту паус меню не активно
    private int score;
    public int lives;
    private float spawnRate = 1.0f;//Интервал с которым появляются новые кубики
    // Start is called before the first frame update
    void Start()
    {
        playMusic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//когда нажимаем эскейп то срабатывает пауза
        {
            if (isGamePaused)
            {
                Resume();             
            }
            else
            {
                PauseGame();
            }
        }

        if (PlayerPrefs.GetInt("Music") == 0)//включение музыки
        {
            mainMusic.enabled = true;
            playMusic = true;

        }
        else if (PlayerPrefs.GetInt("Music") == 1)//выключение музыки
        {
            mainMusic.enabled = false;
            playMusic = false;
        }
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);//спавним через определенное время
            int index = Random.Range(0, targets.Count);// рандомим лист от нуля до стольки скок у нас обьектов
            Instantiate(targets[index]);//спавним тот самый рандомный индекс который выбрался в рандоме на строчке выше     
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);//не показывать меню паузы
        Time.timeScale = 1f;//восстановить время в игре( что бы она дальше работала)
        isGamePaused = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);//Показать интерфейс паузы
        Time.timeScale = 0f;//остановить время в игре
        isGamePaused = true;
    }   

    public void livesScore(int death)//подсчет жизней
    {
        lives -= death;
        livesText.text = "Lives: " + lives;      
    }
    public void UpdateScore(int scoreToAdd)// подсчет счета
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {   
        restartButton.gameObject.SetActive(true);// когда игра усе, появляется надпись
        gameOverText.gameObject.SetActive(true);// когда игра усе, появляется кнопка
        isGameActive = false;//игра вырубается
        MusicOff();
    }
    public void MusicOff()//выключение музыки когда флаг (1)
    {
        if (playMusic)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    public void MusicOn()//включение музыки когда флаг(0)
    {
        if (!playMusic)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Послне нажатия на кнопку рестарт подгружается текущая сцена
        Time.timeScale = 1f;//Убирает баг когда после нажатия клавиши в пауз меню игра не спаунит дальше кубики(остановилось время в игре самой крч)
    }
    public void StartGame(int difficulty)//метод который запускает игру
    {
        isGameActive = true;//игра активна при старте       
        lives = 3;
        score = 0;   
        spawnRate /= difficulty; // подсчет уровня сложности
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        livesScore(0);
        titleScreen.gameObject.SetActive(false);//как только начинается игра вырубаем тайтл скрин
        MusicOn();
    }
    
}
