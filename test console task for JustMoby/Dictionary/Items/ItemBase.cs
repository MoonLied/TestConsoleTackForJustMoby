using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary.ItemsModel
{
    public class ItemBase
    {
        public int ItemId { get; protected set; }
        public ItemType Type { get; protected set; }
        public string ItemName { get; protected set; }

        public ItemBase(JSONNode doc)
        {
            ItemId = doc["id"].AsInt;
            ItemName = doc["name"].Value;
        }
    }
}
