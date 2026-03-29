using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Dendro_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#4CAF50");
    public override ElementType Element => ElementType.Dendro;

    public Dendro_Power(int stacks) : base(stacks) { }
}