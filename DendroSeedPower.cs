using Godot;
using MegaCrit.Sts2.Core.Combat;
using System.Threading.Tasks;

namespace genshin_posion;

public sealed class DendroSeedPower : ElementalPower
{
    public override ElementType Element => ElementType.DendroSeed;
    public override Color AmountLabelColor => Color.FromHtml("#7CB342"); // 草绿

    public DendroSeedPower(int stacks) : base(stacks) { }

    // 草种子不自动减层，等待触发反应
    public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        return Task.CompletedTask;
    }
}