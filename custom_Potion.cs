using Godot;
using MegaCrit.Sts2.Core.Combat;       // 目标类型 TargetType
using MegaCrit.Sts2.Core.Commands;     // 伤害指令 CreatureCmd.Damage
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;   // 生物对象 Creature
using MegaCrit.Sts2.Core.Entities.Potions;    // 药水基础类型
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars; // 伤害变量 DamageVar
using MegaCrit.Sts2.Core.Models;             // 父类 PotionModel
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;      // 变量集合必须
using System.Threading.Tasks; // 异步 await 必须

namespace genshin_posion
{
    //黄金蟹，扣10滴血，获得99层无实体
    public sealed class Golden_Crab : PotionModel
    {
        public override PotionRarity Rarity => PotionRarity.Rare;
        public override PotionUsage Usage => PotionUsage.CombatOnly;
        public override TargetType TargetType => TargetType.Self;

        protected override List<DynamicVar> CanonicalVars =>
        [
            new PowerVar<IntangiblePower>(99m),
        new HpLossVar(10m),
    ];

        protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
        {
            await CreatureCmd.Damage(
                choiceContext,
                Owner.Creature,
                DynamicVars.HpLoss.BaseValue,
                ValueProp.Unpowered,
                Owner.Creature,
                null);
            await PowerCmd.Apply<IntangiblePower>(target, DynamicVars["IntangiblePower"].BaseValue, Owner.Creature, null);
        }
    }
    //蹦蹦炸弹，对群体敌人造成999伤害
    public sealed class BengBengBoom : PotionModel
    {
        
        public override PotionRarity Rarity => PotionRarity.Rare;
        public override PotionUsage Usage => PotionUsage.CombatOnly;
        public override TargetType TargetType => TargetType.AllEnemies;

        protected override List<DynamicVar> CanonicalVars => new List<DynamicVar>
        {
          new DamageVar(999m, ValueProp.Unpowered)
        };

        protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
        {
            await CreatureCmd.Damage(
                choiceContext,
                Owner.Creature.CombatState.HittableEnemies,
                DynamicVars.Damage.BaseValue,
                DynamicVars.Damage.Props,
                Owner.Creature,
                null);
            // 对所有敌人造成伤害
        }
    }
    //蘑幻之菇，死后自动扔出复活并获得10滴血
    public sealed class Suspicious_Mushroom_Phantasm : PotionModel
    {
        public override PotionRarity Rarity => PotionRarity.Rare;
        public override PotionUsage Usage => PotionUsage.Automatic;
        public override TargetType TargetType => TargetType.Self;

        public override bool CanBeGeneratedInCombat => false;
        // 是否可以在战斗结束后生成

        protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
        {
            AssertValidForTargetedPotion(target);
            await CreatureCmd.Heal(target, 10m);
        }
        public override bool ShouldDie(Creature creature)
        {
            return creature != Owner.Creature;
        }
        public override async Task AfterPreventingDeath(Creature creature)
        {
            await OnUseWrapper(new ThrowingPlayerChoiceContext(), creature);
        }
    }
}