using Godot;

namespace genshin_posion;

public sealed class PyroPower : ElementalPower
{
    public override ElementType Element => ElementType.Pyro;
    public override Color AmountLabelColor => Color.FromHtml("#FF5722"); // 火橙

    public PyroPower(int stacks) : base(stacks) { }
}