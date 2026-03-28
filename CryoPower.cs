using Godot;

namespace genshin_posion;

public sealed class CryoPower : ElementalPower
{
    public override ElementType Element => ElementType.Cryo;
    public override Color AmountLabelColor => Color.FromHtml("#00BCD4"); // 冰青

    public CryoPower(int stacks) : base(stacks) { }
}