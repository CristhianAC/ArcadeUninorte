using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Game", menuName = "Game/Game", order = 1)]
public class Game : ScriptableObject
{
    public string SceneName;
    public RawImage image;
    public string nameGame;
    
}
