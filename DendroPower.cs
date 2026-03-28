using Godot;

namespace genshin_posion;

public sealed class DendroPower : ElementalPower
{
    public override ElementType Element => ElementType.Dendro;
    public override Color AmountLabelColor => Color.FromHtml("#8BC34A"); // 草浅绿

    public DendroPower(int stacks) : base(stacks) { }
}