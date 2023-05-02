using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shape", fileName ="New Shape")]
public class ShapeData : ScriptableObject
{
    [SerializeField] private List<ShapeDescription> listShape;

    public List<ShapeDescription> ListShape => listShape;
}
