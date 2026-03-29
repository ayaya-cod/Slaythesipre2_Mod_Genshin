using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace genshin_posion;

public sealed class Anemo_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#80D8F8");
    public override ElementType Element => ElementType.Anemo;

    public Anemo_Power(int stacks) : base(stacks) { }
}