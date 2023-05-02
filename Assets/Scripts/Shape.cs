using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    public float speed;
    public int point;
    public SizeShape size;
    public Texture2D texture;
    public ObjectPool objectPool;

    public abstract void SetData(float speed, int point, float scaleShape, SizeShape size, Texture2D texture);
}
