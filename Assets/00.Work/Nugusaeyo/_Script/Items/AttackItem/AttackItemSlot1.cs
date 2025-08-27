using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackItemSlot1 : ItemTypeProjectile
{
    [Header("Else Detail")]
    public TestPlayer player;
    protected override void SpawnProjectile(Vector3 spawnPosition, TestPlayer player)
    {
        base.SpawnProjectile(spawnPosition, player);
    }
    

    private void Start()
    {
        Debug.Log($@"값 꺼내오기 위한 디버깅 로그
Base.ProjectielName: {base.ProjectileName}
Base.Speed: {base.Speed}
Base.ProjectilePrefab.Name: {base.ProjectilePrefab.name}
Base.AttackItemSO.Name: {base.attackItemSO.name}
Base.AttackItemName: {base.AttackItemName}
Base.ItemID: {base.ItemID}
Base.Damage: {base.Damage}");
        SpawnProjectile(player.transform.position, player);
    }
}
