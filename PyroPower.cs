using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Pyro_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#FF5722");
    public override ElementType Element => ElementType.Pyro;

    public Pyro_Power(int stacks) : base(stacks) { }
}