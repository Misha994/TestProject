using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDestroyer : MonoBehaviour
{
    [SerializeField] private float cameraHeight;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Camera orthoCamera;

    public delegate void DestroyShape(int shapeData);
    public DestroyShape destroyShape;

    private void Start()
    {
        cameraHeight = orthoCamera.orthographicSize * 2f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Якщо луч зіткнувся з об'єктом
            if (hit.collider != null)
            {
                // Отримати посилання на об'єкт
                GameObject hitObject = hit.collider.gameObject;
                Shape shape = hitObject.GetComponent<Shape>();
                destroyShape?.Invoke(shape.point);
                // Знищити об'єкт
                objectPool.ReturnObject(hitObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectPool.ReturnObject(collision.gameObject);
    }

    public void DeleteAllShapes()
    {

    }
}
