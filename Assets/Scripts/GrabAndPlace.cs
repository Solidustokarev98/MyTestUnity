using UnityEngine;
using UnityEngine.UI;

public class GrabAndMove : MonoBehaviour
{
    public Camera playerCamera;            // Ссылка на камеру игрока
    public float grabDistance = 2f;        // Максимальная дистанция для хватания
    public LayerMask grabbableLayer;       // Слой, на котором находятся подбираемые объекты 
    public Image crosshair;                 // Ссылка на UI элемент прицела

    private GameObject grabbedObject;       // Ссылка на схваченный объект 
    private Rigidbody grabbedRigidbody;     // Ссылка на Rigidbody схваченного объекта 
    void Update()
    {
        // Проверка нажатия кнопки "Grab" (например, клавиша E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                PlaceObject();
            }
        }

        // Перемещение схваченного объекта
        if (grabbedObject != null)
        {
            MoveObject();
        }

        // Изменение цвета прицела
        UpdateCrosshair();
    }

    void TryGrabObject()
    {
        RaycastHit hit;

        // Выполняем лучевой каст для обнаружения объектов
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabDistance, grabbableLayer))
        {
            if (hit.transform.CompareTag("Grabbable")) // Убедитесь, что объект имеет тег "Grabbable"
            {
                grabbedObject = hit.transform.gameObject;
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();

                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.isKinematic = true; // Делаем объект кинематическим
                }
            }
        }
    }

    void MoveObject()
    {
        // Перемещение объекта к позиции курсора
        Vector3 worldPosition = playerCamera.transform.position + playerCamera.transform.forward * grabDistance;
        grabbedObject.transform.position = worldPosition;
    }

    void PlaceObject()
    {
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.isKinematic = false; // Возвращаем объект в физический режим 
            grabbedObject = null; // Сбрасываем ссылку на схваченный объект
            grabbedRigidbody = null; // Сбрасываем ссылку на Rigidbody 
        }
    }

    void UpdateCrosshair()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabDistance, grabbableLayer))
        {
            if (hit.transform.CompareTag("Grabbable"))
            {
                crosshair.color = Color.green; // Изменяем цвет прицела на зеленый 
            }
            else
            {
                crosshair.color = Color.white; // Возвращаем цвет прицела в белый
            }
        }
        else {
            crosshair.color = Color.white; // Возвращаем цвет прицела в белый
        }
    }
}
