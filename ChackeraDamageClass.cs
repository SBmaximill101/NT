using Terraria;
using Terraria.ModLoader;

namespace NT.ClassContent
{
    public class ChackraDamageClass : DamageClass
	{
		public static float ChakraCrit = 0;

		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			
			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return new StatInheritanceData(
				damageInheritance: 0f,
				critChanceInheritance: 0f,
				attackSpeedInheritance: 0f,
				armorPenInheritance: 0f,
				knockbackInheritance: 0f
			);
			
		}

		public override void SetDefaultStats(Player player)
		{
			player.GetCritChance<ChackraDamageClass>() = ChakraCrit;
		}

		public override bool UseStandardCritCalcs => true;

	}
}