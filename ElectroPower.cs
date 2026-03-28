using Godot;

namespace genshin_posion;

public sealed class ElectroPower : ElementalPower
{
    public override ElementType Element => ElementType.Electro;
    public override Color AmountLabelColor => Color.FromHtml("#9C27B0"); // 雷紫

    public ElectroPower(int stacks) : base(stacks) { }
}