using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace genshin_posion;

public sealed class Quicken_Power : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool IsInstanced => false;
    public override bool AllowNegative => false;
    public override Color AmountLabelColor => Color.FromHtml("#7C4DFF");

    protected override List<IHoverTip> ExtraHoverTips => new List<IHoverTip>();

    public Quicken_Power(int stacks)
    {
        Amount = stacks;
    }

    // 100%匹配官方`ModifyDamageMultiplicative`API
    public override decimal ModifyDamageMultiplicative(
        Creature target,
        decimal amount,
        ValueProp props,
        Creature dealer,
        CardModel? cardSource)
    {
        if (target != Owner || cardSource == null)
            return amount;

        if (cardSource is IElementCard elementCard &&
            (elementCard.Element is ElementType.Electro or ElementType.Dendro))
            return amount * 1.5m;

        return amount;
    }

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == Owner.Side && Amount > 0)
            await PowerCmd.Decrement(this);
    }
}