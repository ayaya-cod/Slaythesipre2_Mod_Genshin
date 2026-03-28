using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;

namespace genshin_posion;

public sealed class VulnerablePower : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override Color AmountLabelColor => Color.FromHtml("#FF4444");
    public override bool AllowNegative => false;
    public override bool ShouldReceiveCombatHooks => true;
    // 用游戏内置易伤效果的官方Key
    public override LocString Description => new LocString("Powers", "Vulnerable_Description");
    protected override List<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Vulnerable)];

    public VulnerablePower(int stacks)
    {
        base.Amount = stacks;
    }
}