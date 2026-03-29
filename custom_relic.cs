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
    // 西风秘典：每2回合1点能量
    public sealed class Favonius_Codex : RelicModel
    {
        private int _turnsSeen;

        public override RelicRarity Rarity => RelicRarity.Rare;
        public override bool ShowCounter => true;

        public override int DisplayAmount => _turnsSeen;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new EnergyVar(1),
            new DynamicVar("Turns", 2m)
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            new[] { HoverTipFactory.ForEnergy(this) };

        public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
        {
            await base.AfterSideTurnStart(side, combatState);
            if (side == Owner.Creature.Side)
            {
                _turnsSeen = (_turnsSeen + 1) % DynamicVars["Turns"].IntValue;
                Status = (_turnsSeen == DynamicVars["Turns"].IntValue - 1)
                    ? RelicStatus.Active
                    : RelicStatus.Normal;

                if (_turnsSeen == 0)
                {
                    Flash();
                    await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
                }
            }
        }

        public override Task AfterCombatEnd(CombatRoom _)
        {
            _turnsSeen = 0;
            Status = RelicStatus.Normal;
            return Task.CompletedTask;
        }
    }

    // 贯虹之槊：修复敏捷无限叠加
    public sealed class Vortex_Vanquisher : RelicModel
    {
        private bool _hasGrantedDexterity; // 标记是否已给敏捷

        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new PowerVar<DexterityPower>(1m),
            new BlockVar(5m, ValueProp.Unpowered)
        };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
        {
            HoverTipFactory.FromPower<DexterityPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block)
        };

        public override async Task AfterRoomEntered(AbstractRoom room)
        {
            if (room is CombatRoom && !_hasGrantedDexterity)
            {
                Flash();
                await PowerCmd.Apply<DexterityPower>(
                    base.Owner.Creature,
                    base.DynamicVars["DexterityPower"].BaseValue,
                    base.Owner.Creature,
                    null
                );
                _hasGrantedDexterity = true;
            }
        }

        public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
        {
            if (side == base.Owner.Creature.Side)
            {
                int roundNumber = combatState.RoundNumber;
                if (roundNumber >= 1 && roundNumber <= 3)
                {
                    Flash();
                    await CreatureCmd.GainBlock(
                        base.Owner.Creature,
                        base.DynamicVars.Block.BaseValue,
                        ValueProp.Unpowered,
                        null
                    );
                }
            }
        }

        // 战斗结束重置敏捷标记
        public override Task AfterCombatEnd(CombatRoom _)
        {
            _hasGrantedDexterity = false;
            return Task.CompletedTask;
        }
    }
}