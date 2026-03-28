using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace genshin_posion
{
    public sealed class Zhong_Li_Bless : CardModel
    {
        // 多人模式限制
        public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

        // 动态变量：格挡11点
        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
            {
            new BlockVar(5m, ValueProp.Move)
            };

        // 标记这张卡会提供格挡
        public override bool GainsBlock => true;
        public Zhong_Li_Bless()
            : base(
                canonicalEnergyCost: 0,        // 1. 费用：0费
                type: CardType.Skill,          // 2. 类型：技能牌
                rarity: CardRarity.Uncommon,   // 3. 稀有度：罕见
                targetType: TargetType.AnyAlly,// 4. 目标：任意一名队友
                shouldShowInCardLibrary: true  // 5. 在卡牌库显示
            )
        {
        }

        // 卡牌打出效果
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));
            await CreatureCmd.GainBlock(cardPlay.Target, base.DynamicVars.Block, cardPlay);
        }

        // 升级效果：格挡 +5
        protected override void OnUpgrade()
        {
            base.DynamicVars.Block.UpgradeValueBy(5m);
        }
    }
}