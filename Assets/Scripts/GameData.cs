using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData")]
public class GameData : ScriptableObject
{
    public int gameTime;

    public bool winGame=false;
    public bool loseGame = false;
    public bool falled = false;

}
