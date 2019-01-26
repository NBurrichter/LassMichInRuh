using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSettings", menuName = "ScriptableObjects/AttackSettings", order = 1)]
public class EnemyAttackSettings : ScriptableObject
{

    public float playerApproachDistance = 2;

    public float minMoveTime = 1.0f;
    public float maxMoveTime = 2.0f;

    public float minIdleTime = 2.0f;
    public float maxIdleTime = 3.0f;

}
