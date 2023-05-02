using System.Drawing;
using UnityEngine;

public class Circle : Shape
{
    public override void SetData(float speed, int point, float scaleShape, SizeShape size, Texture2D texture)
    {
        this.speed = speed;
        this.point = point;
        transform.localScale = new Vector2(scaleShape, scaleShape);
        GetComponent<Renderer>().material.mainTexture = texture;
        this.size = size;
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y -= speed * Time.deltaTime;

        transform.position = position;
    }
}
