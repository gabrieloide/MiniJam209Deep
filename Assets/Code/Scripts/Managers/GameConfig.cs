using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "OneWayDown/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Firebase / Database Settings")]
    public string databaseUrl;
}
