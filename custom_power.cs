using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace genshin_posion;


public sealed class Blood_Blossom_Aroma : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override Color AmountLabelColor => Color.FromHtml("#5f0509");
    public override bool AllowNegative => false;

 

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
            return;

        await CreatureCmd.Damage(
            new ThrowingPlayerChoiceContext(),
            base.Owner,
            4,
            ValueProp.Unblockable | ValueProp.Unpowered,
            null,
            null
        );

        if (base.Owner.IsAlive)
            await PowerCmd.Decrement(this);
        else
            await Cmd.CustomScaledWait(0.1f, 0.25f);
    }
}