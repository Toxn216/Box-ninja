using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager; //подготовка к вызову скрипта гейм менеджера
    private float minSpeed = 12;//минимальная сила импульса вверх
    private float maxSpeed = 15;//максимальная сила импульса вверх
    private float maxTorque = 10;//скорость вращения ящиков наших
    private float xRange = 4;//позиция спвна по Х
    private float ySpawnPos = -2;//позиция спавна по У 
    

    public ParticleSystem explosionParticle;

    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();//тут мы призвали скрипт гейм менеджера

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);//даем нашим ящикам импульс вверх
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(),ForceMode.Impulse);// крутим наши ящики

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    /*private void OnMouseDown()
    {
        if (gameManager.isGameActive)//мы можем убивать ящики пока игра активна
        {     
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);// вызываем методы гейм менеждера через точку          
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))//когда подает не бомба то гейм овер
        {
            gameManager.livesScore(1);
            if (gameManager.lives <= 0)
            {
                gameManager.GameOver();
                gameManager.livesText.text = "It's not like eating pies=)";
            }
        }
    }
    public void DestroyTarget()//вместо того верхнего метода(что закоментирован) вызываем этот
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);//сила с которой мы запускаем обьект в вверх
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);//Крутим обьект
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);//рандомная позиция по Х и статичная позиция спавна по У
    }
    
}
