using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace genshin_posion;

public sealed class Vulnerable_Power : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool IsInstanced => false;
    public override bool AllowNegative => false;
    public override Color AmountLabelColor => Color.FromHtml("#FF5252");

    protected override List<IHoverTip> ExtraHoverTips => new List<IHoverTip>();

    public Vulnerable_Power(int stacks)
    {
        Amount = stacks;
    }

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == Owner.Side && Amount > 0)
            await PowerCmd.Decrement(this);
    }
}