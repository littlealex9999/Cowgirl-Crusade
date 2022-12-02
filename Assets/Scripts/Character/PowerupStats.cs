using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "Powerup")]
public class PowerupStats : ScriptableObject
{
    [Header("Health Stats")] public float health = 0;
    public float shield = 0;

    [Header("Bullet Attributes"), Space] public float bulletDamageAddition = 0;
    public float bulletDamageMultiplier = 0;
    public float bulletSpeedAddition = 0;
    public float bulletSpeedMultiplier = 0;

    [Header("Special Projectile"), Space] public SpecialProjectile specialSpawn;

    [Header("Time"), Space] public bool permanent;
    public float duration;
}
