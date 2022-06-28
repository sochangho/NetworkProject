using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData 
{
    public const string READY = "ready";
    public const string LOAD = "load";
    public const string CHARACTER = "character";
    public const string Exit = "exit";
    public const string MAP = "map";
    public const string HITPLAYER = "hitplayer";
    

}


public enum CharacterType
{
     type1,
     type2,
     type3,
     type4

    
}


public enum MapType
{
    CubeMap,
    CheseMap,
    WaterMap

}