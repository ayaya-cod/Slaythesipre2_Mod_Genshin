using Godot;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace genshin_posion;

public sealed class FrozenPower : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override Color AmountLabelColor => Color.FromHtml("#E0F7FA");
    public override bool AllowNegative => false;
    public override bool ShouldReceiveCombatHooks => true;
    // 用游戏内置冻结效果的官方Key
    public override LocString Description => new LocString("Powers", "Frozen_Description");

    public FrozenPower(int stacks)
    {
        base.Amount = stacks;
    }

    public override bool ShouldAllowTargeting(Creature target)
    {
        return false;
    }
}