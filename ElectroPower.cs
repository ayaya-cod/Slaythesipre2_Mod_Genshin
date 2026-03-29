using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Electro_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#9C27B0");
    public override ElementType Element => ElementType.Electro;

    public Electro_Power(int stacks) : base(stacks) { }
}