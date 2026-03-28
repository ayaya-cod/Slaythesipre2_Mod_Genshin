using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;

namespace genshin_posion;

public abstract class ElementalPower : PowerModel
{
    // 强制每个元素定义自己的类型，用于反应判断
    public abstract ElementType Element { get; }

    // 通用属性：所有元素共用
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override bool ShouldReceiveCombatHooks => true;

    // 【关键修复】用游戏官方内置的描述Key，不再强制子类重写，彻底解决报错
    // 用游戏原生中毒效果的描述，完全兼容无报错
    public override LocString Description => new LocString("Powers", "Poison_Description");
    // 仅强制子类实现专属颜色
    public abstract override Color AmountLabelColor { get; }

    // 通用构造函数
    protected ElementalPower(int stacks)
    {
        base.Amount = stacks;
    }

    // 通用逻辑：元素每回合自动减层
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side) return;

        if (base.Owner.IsAlive)
            await PowerCmd.Decrement(this);
        else
            await Cmd.CustomScaledWait(0.1f, 0.25f);
    }
}