using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform player;          // Ссылка на объект игрока 
    public float mouseSensitivity = 100f; // Чувствительность мыши 
    public float verticalRotationLimit = 80f; // Ограничение по вертикали

    private float xRotation = 0f;     // Вертикальный угол поворота

    void Start()
    {
        // Скрываем курсор мыши и фиксируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Получаем ввод мыши 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Обрабатываем вертикальный поворот
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit);

        // Применяем поворот к камере
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Поворачиваем игрока по горизонтали
        player.Rotate(Vector3.up * mouseX);
    }
}