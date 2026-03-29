using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using System.Linq;
using System.Threading.Tasks;

namespace genshin_posion;

public static class ElementReactionHelper
{
    public static async Task ApplyElement(PlayerChoiceContext context, Creature target, ElementType elementToApply, int stacks, Creature caster)
    {
        if (target == null || elementToApply == ElementType.None) return;
        var existingPower = target.Powers.FirstOrDefault(p => p is ElementalPower) as ElementalPower;
        var existingElement = existingPower?.Element ?? ElementType.None;

        if (existingElement == ElementType.None) { await AttachElement(context, target, elementToApply, stacks, caster); return; }
        await TriggerReaction(context, target, existingElement, elementToApply, stacks, caster);
        if (existingPower != null) await PowerCmd.Remove(existingPower);
        await AttachElement(context, target, elementToApply, stacks, caster);
    }

    private static async Task AttachElement(PlayerChoiceContext context, Creature target, ElementType element, int stacks, Creature caster)
    {
        switch (element)
        {
            case ElementType.Anemo: await PowerCmd.Apply<Anemo_Power>(target, stacks, caster, null); break;
            case ElementType.Geo: await PowerCmd.Apply<Geo_Power>(target, stacks, caster, null); break;
            case ElementType.Electro: await PowerCmd.Apply<Electro_Power>(target, stacks, caster, null); break;
            case ElementType.Pyro: await PowerCmd.Apply<Pyro_Power>(target, stacks, caster, null); break;
            case ElementType.Hydro: await PowerCmd.Apply<Hydro_Power>(target, stacks, caster, null); break;
            case ElementType.Cryo: await PowerCmd.Apply<Cryo_Power>(target, stacks, caster, null); break;
            case ElementType.Dendro: await PowerCmd.Apply<Dendro_Power>(target, stacks, caster, null); break;
            case ElementType.DendroSeed: await PowerCmd.Apply<Dendro_Seed_Power>(target, stacks, caster, null); break;
        }
    }

    private static async Task TriggerReaction(PlayerChoiceContext context, Creature target, ElementType existing, ElementType apply, int stacks, Creature caster)
    {
        switch (existing, apply)
        {
            case (ElementType.Hydro, ElementType.Pyro): await CreatureCmd.Damage(context, target, stacks * 1.5m, ValueProp.Unblockable, caster, null); break;
            case (ElementType.Pyro, ElementType.Hydro): await CreatureCmd.Damage(context, target, stacks * 2m, ValueProp.Unblockable, caster, null); break;
            case (ElementType.Dendro, ElementType.Electro): case (ElementType.Electro, ElementType.Dendro): await PowerCmd.Apply<Quicken_Power>(target, stacks, caster, null); break;
            case (ElementType.Cryo, ElementType.Hydro): case (ElementType.Hydro, ElementType.Cryo): await PowerCmd.Apply<Frozen_Power>(target, stacks, caster, null); break;
            case (ElementType.Cryo, ElementType.Electro): case (ElementType.Electro, ElementType.Cryo): await PowerCmd.Apply<Vulnerable_Power>(target, stacks, caster, null); break;
        }
    }
}