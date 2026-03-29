using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Dendro_Seed_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#8BC34A");
    public override ElementType Element => ElementType.DendroSeed;

    public Dendro_Seed_Power(int stacks) : base(stacks) { }
}