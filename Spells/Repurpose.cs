using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbonsUnfixerMod.Spells
{
    internal class Repurpose
    {
        internal static void Configure()
        {
            BuffConfigurator.For(BuffRefs.RepurposeBuffUndead)
                .RemoveComponents(c => c is RemoveFeatureOnApply)
                .Configure();
        }
    }
}
