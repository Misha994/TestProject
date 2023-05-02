using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float cameraWidth;
    [SerializeField] private float cameraHeight;

    [SerializeField] private Camera orthoCamera;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private ShapeData shapeData;
    [SerializeField] private TextureGeneration textureGeneration;
    [SerializeField] private ObjectPool objectPool;

    [SerializeField] private SizeShape[] SizeShapes;

    private Coroutine spawnCoroutine;

    [SerializeField] private int amountShapes;
    private float newSpeed;
    private int newPoint;

    private void Start()
    {
        cameraHeight = orthoCamera.orthographicSize * 2f;
        cameraWidth = cameraHeight * orthoCamera.aspect;
        InitializeObjectPool();
        GenerationTexture();
    }

    public void ChangeLevel(int poin, float speed, int amountShapes)
    {
        textureGeneration.ChangeTexture();
        GenerationTexture();
        this.amountShapes += amountShapes;
        newSpeed += speed;
        newPoint += poin;
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            for (int i = 0; i < amountShapes; i++)
            {
                if (SizeShapes.Length == shapeData.ListShape.Count)
                {
                    SpawnShape(shapeData.ListShape[Random.Range(0, shapeData.ListShape.Count)]);
                }
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void GenerationTexture()
    {
        for (int i = 0; i < shapeData.ListShape.Count; i++)
        {
            AddTexture(shapeData.ListShape[i]);
        }
    }

    public void StartSpawn()
    {
        spawnCoroutine = StartCoroutine(SpawnObjects());
    }

    public void StopSpawn()
    {
        StopCoroutine(spawnCoroutine);
    }

    public async void AddTexture(ShapeDescription shapeData)
    {
        await textureGeneration.GenerateTexture(shapeData.resolution, shapeData.size);
    }

    public void SpawnShape(ShapeDescription shapeData)
    {
        float circleRadius = shapeData.scaleShape / 2;
        Texture2D newTexture = textureGeneration.GetTexture(shapeData.size);

        Vector3 randomPosition = new Vector3(Random.Range(-cameraWidth / 2f - circleRadius, cameraWidth / 2f + circleRadius), cameraHeight / 2f + circleRadius, 0f);

        GameObject newCircle = objectPool.GetObject(shapeData.size);
        newCircle.transform.position = randomPosition;

        newCircle.GetComponent<Shape>().SetData(shapeData.speed + newSpeed, shapeData.point + newPoint, shapeData.scaleShape, shapeData.size, newTexture);

        // Check if the circle is outside of the camera bounds and clamp it to the edges if necessary
        float clampX = Mathf.Clamp(newCircle.transform.position.x, -cameraWidth / 2f + circleRadius, cameraWidth / 2f - circleRadius);
        newCircle.transform.position = new Vector3(clampX, cameraHeight / 2f + circleRadius, 0f);
    }

    public void InitializeObjectPool()
    {
        for (int i = 0; i < SizeShapes.Length; i++)
        {
            objectPool.InitializeObjectPool(SizeShapes[i], circlePrefab);
        }
    }

    public void DeleteAllShapes()
    {
        objectPool.ClearAllObject();
        textureGeneration.ResetColor();
        textureGeneration.ChangeTexture();
        GenerationTexture();
        amountShapes = 1;
        newSpeed = 0;
        newPoint = 0;
    }

}
