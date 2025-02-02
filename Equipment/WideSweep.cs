using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using BlueprintCore.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.UnitLogic.Commands.Base;
using System.Data;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace EbonsUnfixerMod.Equipment
{
    internal class WideSweep
    {
        private const string WideSweepDescription = "WideSweep.Description";

        internal static void Configure()
        {
            ItemWeaponConfigurator.For(ItemWeaponRefs.WideSweepItem)
                .SetDescriptionText(WideSweepDescription)
                .Configure();
            
            var actions = ActionsBuilder.New()
                .Conditional(
                    ConditionsBuilder.New().IsCaster(true).IsMainTarget(true),
                    ifTrue: ActionsBuilder.New().DealWeaponDamage(false))
                .Build();
            
            AbilityConfigurator.For(AbilityRefs.WideSweepAbility)
                .RemoveComponents(c => c is AbilityEffectRunAction)
                .Configure();

            AbilityConfigurator.For(AbilityRefs.WideSweepAbility)
                .AddAbilityEffectRunAction(actions)
                .Configure(delayed: true);
        }
    }
}
