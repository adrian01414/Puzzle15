using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField, MinValue(3), MaxValue(10)] public int Size { get; private set; } = 3;
}
