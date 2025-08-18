namespace _00.Work.lusalord._02.Script
{
    //패시브 아이템 능력치
    public enum StateType
    {
        //공격 관련
        Atk,
        AtkSpeed,
        CriticalChance,
        
        //플레이어 관련
        Hp,
        Speed,
        JumpForce,
    }

    [System.Serializable]
    public struct StatModifier
    {
        public StateType stateType;
        public float value;
    }
    
}
