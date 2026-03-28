using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace genshin_posion;

public sealed class QuickenPower : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override Color AmountLabelColor => Color.FromHtml("#7C4DFF");
    public override bool AllowNegative => false;
    public override bool ShouldReceiveCombatHooks => true;
    // 用游戏内置增伤效果的官方Key
    public override LocString Description => new LocString("Powers", "DamageBoost_Description");

    public QuickenPower(int stacks)
    {
        base.Amount = stacks;
    }
}