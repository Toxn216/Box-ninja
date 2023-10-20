using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;//��������� ����� � ������
    public TextMeshProUGUI gameOverText; //��������� ����� � ���� ������
    public TextMeshProUGUI livesText; //����� � �������   
    public Button restartButton;// ��������� ������ ��������
    public GameObject titleScreen;// ��������� ������ �����
    public GameObject pauseMenuUI;//��������� ���� ����
    public AudioSource mainMusic;//������ ������� ������ �� ��������
    public bool playMusic;//������ ����� ������
    public bool isGameActive;//������������ �� ����
    public static bool isGamePaused = false;// �� ������� ���� ���� �� �������
    private int score;
    public int lives;
    private float spawnRate = 1.0f;//�������� � ������� ���������� ����� ������
    // Start is called before the first frame update
    void Start()
    {
        playMusic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//����� �������� ������ �� ����������� �����
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

        if (PlayerPrefs.GetInt("Music") == 0)//��������� ������
        {
            mainMusic.enabled = true;
            playMusic = true;

        }
        else if (PlayerPrefs.GetInt("Music") == 1)//���������� ������
        {
            mainMusic.enabled = false;
            playMusic = false;
        }
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);//������� ����� ������������ �����
            int index = Random.Range(0, targets.Count);// �������� ���� �� ���� �� ������� ���� � ��� ��������
            Instantiate(targets[index]);//������� ��� ����� ��������� ������ ������� �������� � ������� �� ������� ����     
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);//�� ���������� ���� �����
        Time.timeScale = 1f;//������������ ����� � ����( ��� �� ��� ������ ��������)
        isGamePaused = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);//�������� ��������� �����
        Time.timeScale = 0f;//���������� ����� � ����
        isGamePaused = true;
    }   

    public void livesScore(int death)//������� ������
    {
        lives -= death;
        livesText.text = "Lives: " + lives;      
    }
    public void UpdateScore(int scoreToAdd)// ������� �����
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {   
        restartButton.gameObject.SetActive(true);// ����� ���� ���, ���������� �������
        gameOverText.gameObject.SetActive(true);// ����� ���� ���, ���������� ������
        isGameActive = false;//���� ����������
        MusicOff();
    }
    public void MusicOff()//���������� ������ ����� ���� (1)
    {
        if (playMusic)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    public void MusicOn()//��������� ������ ����� ����(0)
    {
        if (!playMusic)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//������ ������� �� ������ ������� ������������ ������� �����
        Time.timeScale = 1f;//������� ��� ����� ����� ������� ������� � ���� ���� ���� �� ������� ������ ������(������������ ����� � ���� ����� ���)
    }
    public void StartGame(int difficulty)//����� ������� ��������� ����
    {
        isGameActive = true;//���� ������� ��� ������       
        lives = 3;
        score = 0;   
        spawnRate /= difficulty; // ������� ������ ���������
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        livesScore(0);
        titleScreen.gameObject.SetActive(false);//��� ������ ���������� ���� �������� ����� �����
        MusicOn();
    }
    
}
