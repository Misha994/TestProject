using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TextureGeneration : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float collorIndex;
    [SerializeField] private Vector2 offset;
    [SerializeField] private FilterMode filterMode;
    [SerializeField] private TextureWrapMode textureWrapMode;
    [SerializeField] private Gradient colorGradient;

    private Dictionary<SizeShape, Texture2D> textureArray = new Dictionary<SizeShape, Texture2D>();
    private float newCollorIndex = 0;

    public Texture2D GetTexture(SizeShape size)
    {
        return textureArray[size];
    }

    public async Task GenerateTexture(int resolutiones, SizeShape sizeShape)
    {
        Color32[] colors = new Color32[resolutiones * resolutiones];
        Texture2D texture = new Texture2D(resolutiones, resolutiones);

        texture.filterMode = filterMode;
        texture.wrapMode = textureWrapMode;

        if (texture.width != resolutiones)
        {
            texture.Resize(resolutiones, resolutiones);
        }

        float step = 1f / resolutiones;
        await Task.Run(() =>
        {
            for (int x = 0; x < resolutiones; x++)
            {
                for (int y = 0; y < resolutiones; y++)
                {
                    float x2 = Mathf.Pow((x + 0.5f) * step - offset.x, 2);
                    float y2 = Mathf.Pow((y + 0.5f) * step - offset.y, 2);
                    float r2 = Mathf.Pow(radius, 2);

                    if (x2 + y2 < r2)
                    {
                        colors[x + y * resolutiones] = colorGradient.Evaluate(newCollorIndex);
                    }
                    else
                    {
                        colors[x + y * resolutiones] = Color.clear;
                    }
                }
            }
        });

        texture.SetPixels32(colors);
        texture.Apply();

        textureArray.Add(sizeShape, texture);
    }

    public void ChangeTexture()
    {
        newCollorIndex += collorIndex;
        textureArray.Clear();
    }

    public void ResetColor()
    {
        newCollorIndex = 0;
    }
}
