using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Hydro_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#2196F3");
    public override ElementType Element => ElementType.Hydro;

    public Hydro_Power(int stacks) : base(stacks) { }
}