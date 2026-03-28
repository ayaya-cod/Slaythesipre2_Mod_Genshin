using Godot;

namespace genshin_posion;

public sealed class GeoPower : ElementalPower
{
    public override ElementType Element => ElementType.Geo;
    public override Color AmountLabelColor => Color.FromHtml("#FFC107"); // 岩黄

    public GeoPower(int stacks) : base(stacks) { }
}