using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace genshin_posion
{
    // 西风秘典：每2回合获得1点能量（本场战斗计数，战斗结束重置）
    public sealed class Favonius_Codex : RelicModel
    {
        // 每 2 回合触发一次
        public const int turnsThreshold = 2;

        private bool _isActivating;
        private int _turnsSeen;

        // 稀有度
        public override RelicRarity Rarity => RelicRarity.Rare;

        // 显示计数器
        public override bool ShowCounter => true;

        // 计数器显示内容（完全仿照开心小花）
        public override int DisplayAmount
        {
            get
            {
                if (!IsActivating)
                {
                    return TurnsSeen;
                }
                return DynamicVars["Turns"].IntValue;
            }
        }

        // 动态变量：能量 + 回合数
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new EnergyVar(1),
            new DynamicVar("Turns", 2m)
        ];

        // 悬浮提示显示能量图标
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            new[] { HoverTipFactory.ForEnergy(this) };

        // 激活状态
        private bool IsActivating
        {
            get => _isActivating;
            set
            {
                AssertMutable();
                _isActivating = value;
                InvokeDisplayAmountChanged();
            }
        }

        // 当前回合计数
        public int TurnsSeen
        {
            get => _turnsSeen;
            set
            {
                AssertMutable();
                _turnsSeen = value;
                InvokeDisplayAmountChanged();
            }
        }

        // 回合开始触发
        public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
        {
            await base.AfterSideTurnStart(side, combatState);

            // 只在自己的回合
            if (side == Owner.Creature.Side)
            {
                // 回合计数 +1，对2取余（0→1→0→1）
                TurnsSeen = (TurnsSeen + 1) % DynamicVars["Turns"].IntValue;

                // 下回合即将触发 → 高亮
                Status = (TurnsSeen == DynamicVars["Turns"].IntValue - 1)
                    ? RelicStatus.Active
                    : RelicStatus.Normal;

                // 触发效果
                if (TurnsSeen == 0)
                {
                    Flash();
                    await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
                }
            }
        }

        // 战斗结束 → 重置计数器（不跨战斗）
        public override Task AfterCombatEnd(CombatRoom _)
        {
            TurnsSeen = 0;
            Status = RelicStatus.Normal;
            return Task.CompletedTask;
        }
    }
    //贯虹之槊，给与一点敏捷并且前三回合给与五点格挡
    public sealed class Vortex_Vanquisher : RelicModel
    {

        public override RelicRarity Rarity => RelicRarity.Rare;


        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new PowerVar<DexterityPower>(1m),
            new BlockVar(5m, ValueProp.Unpowered)
        };

        /// <summary>
        /// 鼠标悬浮提示：敏捷图标 + 格挡图标
        /// </summary>
        protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
        {
            HoverTipFactory.FromPower<DexterityPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block)
        };

        /// <summary>
        /// 进入房间时触发（照搬 OddlySmoothStone）
        /// 如果是战斗房间，给予1点敏捷
        /// </summary>
        public override async Task AfterRoomEntered(AbstractRoom room)
        {
            if (room is CombatRoom)
            {
                Flash();
                await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null);
            }
        }

        public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
        {
            // 只在自己的回合生效
            if (side == base.Owner.Creature.Side)
            {
                // 当前回合数
                int roundNumber = combatState.RoundNumber;

                // 只在前三回合生效
                if (roundNumber >= 1 && roundNumber <= 3)
                {
                    Flash();
                    // 获得格挡，使用动态变量中的 Block 值
                    await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block.BaseValue, ValueProp.Unpowered, null);
                }
            }
        }
    }
}