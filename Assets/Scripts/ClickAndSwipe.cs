using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;// ��������� ���� ��������
    private Camera cam; //������
    private Vector3 mousePos; // ������� �����
    private TrailRenderer traill;//��� � ����� �������� ����� ������� �� ������ �������
    private BoxCollider col; //�������� �������� ��� � �����(�� ��� �� �����)

    private bool swiping = false; //���� �������� �� �� ��
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main; //��������� ��� ����:
        traill = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        traill.enabled = false;
        col.enabled = false;

        gameManager= GameObject.Find("Game Manager").GetComponent<GameManager>();//������� ���� ��������
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))//����� ����� ������ �� �������� �������� ������
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))//����� ����� �� ������ ���������� �������� ������
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)//��� � ����� �������� ���� ����������� ������� ����� �� ������
            {
                UpdateMousePosition();
            }
        }
    }
    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;//��� ����� �������... �� ��� � ����� � ������� ������ �� ����������� �������������� ����� �� ������
    }
    void UpdateComponents()//��� � ����� ����� �������� ������� ����� ��������
    {
        traill.enabled = swiping;
        col.enabled = swiping;
    }
    private void OnCollisionEnter(Collision collision)//��� ������������ ����� ������� ��� � ���� �����������
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            //destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
