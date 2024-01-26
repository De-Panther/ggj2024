using UnityEngine;

namespace Progress
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public float gameDuration = 60f; // Duration of the game in seconds
    }
}