using UnityEngine;

public abstract class ItemTypeProjectile : TestAttackItem
{
    [Header("Projectile Settings")]
    public GameObject ProjectilePrefab;
    public string ProjectileName;
    public float Speed;

    private GameObject Projectile;
    protected virtual void SpawnProjectile(Vector3 spawnPosition,TestPlayer player)
    {
        Projectile = Instantiate(ProjectilePrefab,
            this.transform);
        Projectile.name = ProjectileName;
        Rigidbody2D projectileRb =  Projectile.GetComponent<Rigidbody2D>();
        projectileRb.linearVelocityX = Speed * player.MoveDir.normalized.x;
    }
}
