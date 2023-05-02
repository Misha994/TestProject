using UnityEngine;

public class ShapeFactory : MonoBehaviour
{
    [SerializeField] private GameObject cirkl;
    public Shape CreatShape(SizeShape sizeShape, Vector3 point)
    {
        switch (sizeShape)
        {
            case SizeShape.Little:
                GameObject obj = Instantiate(cirkl, point, Quaternion.identity);
                return obj.GetComponent<Shape>();
            default:
                return null;
        }
    }
}
