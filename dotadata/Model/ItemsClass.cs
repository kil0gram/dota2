using dotadata.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace dotadata.Model
{
    public class ItemsClass
    {

        public class Item
        {
            public string name { get; set; }
            public string id { get; set; }
            public string castrange { get; set; }
            public string castpoint { get; set; }
            public string cooldown { get; set; }
            public string manacost { get; set; }

            public string ItemCost { get; set; }
            public string ItemShopTags { get; set; }
            public string ItemQuality { get; set; }
            public string ItemAliases { get; set; }
            public string ItemStackable { get; set; }
            public string ItemShareability { get; set; }

            public string ItemPermanent { get; set; }
            public string ItemInitialCharges { get; set; }
            public string SideShop { get; set; }
            public string AbilitySharedCooldown { get; set; }
            public string AbilityChannelTime { get; set; }
        }


        public static List<ItemsClass.Item> ParseItemsText(string[] text)
        {
            bool itemfound = false;

            //list to hold our parsed items.
            List<Model.ItemsClass.Item> items = new List<Model.ItemsClass.Item>();

            //this will be used to store this sectoion
            //of the item.
            List<string> curitem = new List<string>();

            //item object will will be populating
            Model.ItemsClass.Item item = new Model.ItemsClass.Item();

            //lets go line by line to start parsing.
            foreach (string line in text)
            {
                //clean up the text, remove quotes.
                string line_noquotes = line.Replace("\"", "");
                string trimmed_clean = line.Replace("\"", "").Replace("\t", "").Replace("_"," ").Trim();

                //if line starts with item_ then
                //this is where we will start capturing.
                if (line_noquotes.StartsWith("	item_"))
                {
                    item = new Model.ItemsClass.Item();
                    itemfound = true;
                    item.name = trimmed_clean.Replace("item ", "");
                    item.name = StringManipulation.UppercaseFirst(item.name);
                    curitem.Add(trimmed_clean);
                }

                //if we are on a current item then lets do
                //some other operations to gather details
                if (itemfound == true)
                {
                    //parse ID
                    if (trimmed_clean.StartsWith("ID"))
                    {
                        item.id = trimmed_clean.Replace("ID", "").Split('/')[0];
                        curitem.Add(line);
                    }

                    //parse cast range
                    if (trimmed_clean.StartsWith("AbilityCastRange"))
                    {
                        item.castrange = trimmed_clean.Replace("AbilityCastRange", "");
                        curitem.Add(trimmed_clean);
                    }

                    //parse cast point
                    if (trimmed_clean.StartsWith("AbilityCastPoint"))
                    {
                        item.castpoint = trimmed_clean.Replace("AbilityCastPoint", "");
                        curitem.Add(trimmed_clean);
                    }

                    //parse cast point
                    if (trimmed_clean.StartsWith("AbilityCooldown"))
                    {
                        item.cooldown = trimmed_clean.Replace("AbilityCooldown", "");
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("AbilityManaCost"))
                    {
                        item.manacost = trimmed_clean.Replace("AbilityManaCost", "");
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("ItemCost"))
                    {
                        item.ItemCost = trimmed_clean.Replace("ItemCost", "");
                        curitem.Add(trimmed_clean);
                    }
                    //
                    if (trimmed_clean.StartsWith("ItemShopTags"))
                    {
                        item.ItemShopTags = trimmed_clean.Replace("ItemShopTags", "");
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("ItemQuality"))
                    {
                        item.ItemQuality = trimmed_clean.Replace("ItemQuality", ""); ;
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("ItemAliases"))
                    {
                        item.ItemAliases = trimmed_clean.Replace("ItemAliases", ""); ;
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("ItemStackable"))
                    {
                        item.ItemStackable = trimmed_clean.Replace("ItemStackable", ""); ;
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("ItemShareability"))
                    {
                        item.ItemShareability = trimmed_clean.Replace("ItemShareability", ""); ;
                        curitem.Add(trimmed_clean);
                    }

                    if (trimmed_clean.StartsWith("ItemShareability"))
                    {
                        item.ItemShareability = trimmed_clean.Replace("ItemShareability", ""); ;
                        curitem.Add(trimmed_clean);
                    }

                    //end current item, save to list
                    if (trimmed_clean.StartsWith("//="))
                    {
                       

                        //add to our list of items/
                        items.Add(item);
                        curitem.Add(trimmed_clean);
                        itemfound = false;
                    }

                }

            }

            return items;
        }
    }
}
