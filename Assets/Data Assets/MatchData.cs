using UnityEngine;

[CreateAssetMenu(fileName = "MatchDataData", menuName = "Match Data")]
public class MatchData : ScriptableObject
{
    public float timeToSpawnLegion;
    public float timeToSpawnGolem;
    public float timeToSpawnMonsterDuo;
    public float timeToSpawnDragon;
    public float timeToSpawnTreasureChestMonster;
    public float timeToSpawnHero;
    public float timeToActiveTower;
    public float timeToRevivalDragon;
    public float timeToRevivalGolem;
    public float timeToRevivalTreasureChestMonster;
    public float timeToRevivalMonsterDuo;
    public float timeToRevivalHeroMin;
    public float timeToRevivalHeroMax;
    public float timeToBetweenMultiKill;
    public float totalTime;
    public float timeOff;

}