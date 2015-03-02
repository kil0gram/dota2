using dotadata.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotadata.Model
{
    public class AbilitiesClass
    {
        public class Ability
        {
            public string Name { get; set; }
            public string HeroName { get; set; }
            public string ID { get; set; }
            public string AbilityType { get; set; }
            public string AbilityBehavior { get; set; }
            public string OnCastbar { get; set; }
            public string OnLearnbar { get; set; }

            public string FightRecapLevel { get; set; }
            public string AbilityUnitTargetTeam { get; set; }
            public string AbilityUnitTargetType { get; set; }
            public string AbilityUnitDamageType { get; set; }
            public string AbilityCastRange { get; set; }
            public string AbilityCastRangeBuffer { get; set; }
            public string AbilityCastPoint { get; set; }
            public string AbilityChannelTime { get; set; }
            public string AbilityCooldown { get; set; }

            public string AbilityDuration { get; set; }
            public string AbilitySharedCooldown { get; set; }
            public string AbilityDamage { get; set; }
            public string AbilityManaCost { get; set; }
            public string AbilityModifierSupportValue { get; set; }

            public string ItemCost { get; set; }
            public string ItemInitialCharges { get; set; }
            public string ItemCombinable { get; set; }
            public string ItemPermanent { get; set; }
            public string ItemStackable { get; set; }

            public string ItemRecipe { get; set; }
            public string ItemDroppable { get; set; }
            public string ItemPurchasable { get; set; }
            public string ItemSellable { get; set; }
            public string ItemRequiresCharges { get; set; }

            public string ItemKillable { get; set; }
            public string ItemDisassemblable { get; set; }
            public string ItemShareability { get; set; }
            public string ItemDeclaresPurchase { get; set; }
        }



        public static List<AbilitiesClass.Ability> ParseAbilityText(string[] text)
        {
            bool itemfound = false;

            //list to hold our parsed items.
            List<AbilitiesClass.Ability> abilities = new List<AbilitiesClass.Ability>();

            //item object will will be populating
            AbilitiesClass.Ability ability = new AbilitiesClass.Ability();

            //lets go line by line to start parsing.
            int count = 0;
            foreach (string line in text)
            {
                //clean up the text, remove quotes.
                
                string trimmed_clean = line.Replace("\"", "").Replace("\t", "").Trim();

                //if line starts with item_ then
                //this is where we will start capturing.
                if (trimmed_clean.StartsWith("ID"))
                {
                    ability = new AbilitiesClass.Ability();
                    itemfound = true;
                    ability.ID = trimmed_clean.Replace("ID", "").Split('/')[0].Trim();

                    //we get the hero name
                    ability.HeroName = text[count - 4].Split('_')[0].Replace("\"", "").Replace("\t", "").Trim();
                   
                    //lets remove hero name from original 
                    ability.Name = text[count - 4].Replace(ability.HeroName, "").Replace("_", " ").Replace("\"", "").Replace("\t", "").Trim();
                }

                //if we are on a current item then lets do
                //some other operations to gather details
                if (itemfound == true)
                {

                    //parse
                    if (trimmed_clean.StartsWith("AbilityBehavior"))
                    {
                        ability.AbilityBehavior = trimmed_clean.Replace("AbilityBehavior", "");
                    }


                    if (trimmed_clean.StartsWith("AbilityUnitTargetTeam"))
                    { ability.AbilityUnitTargetTeam = trimmed_clean.Replace("AbilityUnitTargetTeam", ""); }
                    if (trimmed_clean.StartsWith("AbilityUnitTargetType"))
                    { ability.AbilityUnitTargetType = trimmed_clean.Replace("AbilityUnitTargetType", ""); }
                    if (trimmed_clean.StartsWith("AbilityUnitDamageType"))
                    { ability.AbilityUnitDamageType = trimmed_clean.Replace("AbilityUnitDamageType", ""); }
                    if (trimmed_clean.StartsWith("AbilityCastRange"))
                    { ability.AbilityCastRange = trimmed_clean.Replace("AbilityCastRange", ""); }
                    if (trimmed_clean.StartsWith("AbilityCastPoint"))
                    { ability.AbilityCastPoint = trimmed_clean.Replace("AbilityCastPoint", ""); }
                    if (trimmed_clean.StartsWith("AbilityCooldown"))
                    { ability.AbilityCooldown = trimmed_clean.Replace("AbilityCooldown", ""); }
                    if (trimmed_clean.StartsWith("AbilityManaCost"))
                    { ability.AbilityManaCost = trimmed_clean.Replace("AbilityManaCost", ""); }
                   
                    //end current item, save to list
                    if (trimmed_clean.StartsWith("//="))
                    {
                        //add to our list of items/
                        abilities.Add(ability);
                        
                        itemfound = false;
                    }

                }
                count++;
            }

            return abilities;
        }
    }
}
