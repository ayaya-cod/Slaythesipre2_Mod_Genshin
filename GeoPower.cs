using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;
using System.Threading.Tasks;

namespace genshin_posion;

public sealed class Geo_Power : ElementalPower
{
    public override Color AmountLabelColor => Color.FromHtml("#FBC02D");
    public override ElementType Element => ElementType.Geo;

    public Geo_Power(int stacks) : base(stacks) { }
}