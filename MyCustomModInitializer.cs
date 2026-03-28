using genshin_posion;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace genshin_posion
{
    [ModInitializer(nameof(Initialize))]
    public static class MyCustomModInitializer
    {
        public static void Initialize()
        {
            Log.Info("MyCustomMod - 加载成功!");
            ModHelper.AddModelToPool(typeof(SharedPotionPool), typeof(BengBengBoom));
            ModHelper.AddModelToPool(typeof(SharedPotionPool), typeof(Golden_Crab));
            ModHelper.AddModelToPool(typeof(SharedPotionPool), typeof(Suspicious_Mushroom_Phantasm));
            ModHelper.AddModelToPool(typeof(SharedRelicPool), typeof(Favonius_Codex));
            ModHelper.AddModelToPool(typeof(SharedRelicPool), typeof(Vortex_Vanquisher));
            ModHelper.AddModelToPool(typeof(ColorlessCardPool), typeof(Zhong_Li_Bless));
           

        }
    }
}