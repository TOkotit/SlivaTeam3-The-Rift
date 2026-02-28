namespace Entity
{
    public interface IAttackProfile
    {
        string Name { get; set; }
        float  AttackUptime {get; set;}
        float  Cooldown {get; set;}
    }
}