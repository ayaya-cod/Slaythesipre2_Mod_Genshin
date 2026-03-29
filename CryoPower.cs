using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Cryo_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#B3E5FC");
    public override ElementType Element => ElementType.Cryo;

    public Cryo_Power(int stacks) : base(stacks) { }
}