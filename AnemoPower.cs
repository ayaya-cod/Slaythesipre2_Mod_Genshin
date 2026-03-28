using Godot;

namespace genshin_posion;

public sealed class AnemoPower : ElementalPower
{
    public override ElementType Element => ElementType.Anemo;
    public override Color AmountLabelColor => Color.FromHtml("#4CAF50"); // 风绿

    public AnemoPower(int stacks) : base(stacks) { }
}