using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;         // Скорость движения 
    public float jumpHeight = 2f;        // Высота прыжка 
    public float gravity = -9.81f;        // Гравитация 
    private CharacterController controller; // Компонент CharacterController
    private Vector3 velocity;               // Вектор скорости
    private bool isGrounded;                // Флаг для проверки, на земле ли персонаж

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Проверяем, на земле ли персонаж isGrounded = controller.isGrounded;

        // Если персонаж на земле, сбрасываем вертикальную скорость
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Получаем ввод от пользователя
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Создаем вектор движения
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Двигаем персонажа
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Обработка прыжка
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Применяем гравитацию velocity.y += gravity * Time.deltaTime;

        // Двигаем персонажа с учетом гравитации
        controller.Move(velocity * Time.deltaTime);
    }
}
