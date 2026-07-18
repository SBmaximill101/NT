using Terraria;
using Terraria.ModLoader;

namespace NT.ClassContent
{
    internal class NinjaToolDamageClass : DamageClass
    {
		public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			
			if (damageClass == ModContent.GetInstance<ChackraDamageClass>())
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
			
		}

		
		public override bool UseStandardCritCalcs => true;

	}
}
