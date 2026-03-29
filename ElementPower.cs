using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;

namespace genshin_posion;

public abstract class ElementalPower : PowerModel
{
    // 仅子类必须实现的抽象成员（无多余重写）
    public abstract ElementType Element { get; }
    public abstract override Color AmountLabelColor { get; }

    // 通用属性（完全匹配官方API，子类无需重复定义）
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool IsInstanced => false;
    public override bool AllowNegative => false;

    // 通用构造函数（子类必须调用）
    protected ElementalPower(int stacks)
    {
        Amount = stacks;
    }

    // 通用回合减层逻辑（完全匹配官方`AfterSideTurnStart`）
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == Owner.Side && Amount > 0 && Owner.IsAlive)
            await PowerCmd.Decrement(this);
    }
}