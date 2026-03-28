using Godot;

namespace genshin_posion;

public sealed class HydroPower : ElementalPower
{
    public override ElementType Element => ElementType.Hydro;
    public override Color AmountLabelColor => Color.FromHtml("#2196F3"); // 水蓝

    public HydroPower(int stacks) : base(stacks) { }
}