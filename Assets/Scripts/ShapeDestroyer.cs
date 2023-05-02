using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDestroyer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ObjectPool objectPool;

    public delegate void DestroyShape(int shapeData);
    public DestroyShape destroyShape;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                Shape shape = hitObject.GetComponent<Shape>();
                destroyShape?.Invoke(shape.point);
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
