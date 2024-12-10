using UnityEngine;

public class TurretSelection : MonoBehaviour
{
    public GameObject panel;


    void Start()
    {
        panel.SetActive(false); // ��������� ������ ��� �������
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(false);
        }
    }
}
