namespace genshin_posion;

/// <summary>
/// 元素卡牌标记接口：所有元素攻击卡必须实现
/// </summary>
public interface IElementCard
{
    ElementType Element { get; }
}