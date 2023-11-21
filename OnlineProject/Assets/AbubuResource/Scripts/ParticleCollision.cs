using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    EnemyMove enemyMove;
    private void OnParticleCollision(GameObject other)
    {
        other.GetComponent<EnemyMove>().ParticleDamage();
        other.GetComponent<BossEnemy>().ParticleDamage();
        other.GetComponent<WaterEnemy>().ParticleDamage();
    }
}
