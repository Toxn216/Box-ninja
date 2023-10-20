using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;// подрубаем гейм менеджер
    private Camera cam; //камера
    private Vector3 mousePos; // позиция мышки
    private TrailRenderer traill;//как я понял анимация самой дорожки за мышкой которое
    private BoxCollider col; //колайдер анимации как я понил(но это не точно)

    private bool swiping = false; //флаг свайпаем мы чи не
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main; //подрубаем всю тему:
        traill = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        traill.enabled = false;
        col.enabled = false;

        gameManager= GameObject.Find("Game Manager").GetComponent<GameManager>();//находим гейм менеджер
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))//когда мышка нажата то начинаем анимацию свайпа
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))//когда мышка не нажата прекращаем анимацию свайпа
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)//как я понял свайпинг этот отслеживает позицию мауса по методу
            {
                UpdateMousePosition();
            }
        }
    }
    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;//все очень страшно... но как я понял с помощью камеры мы отслеживаем местоположение мышки на экране
    }
    void UpdateComponents()//как я понял метод вызывает видимую часть анимации
    {
        traill.enabled = swiping;
        col.enabled = swiping;
    }
    private void OnCollisionEnter(Collision collision)//при столкновении мышки убивать все к чему притронется
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            //destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
