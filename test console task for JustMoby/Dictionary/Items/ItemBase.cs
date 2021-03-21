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

        public ItemBase(JSONNode doc, ItemType type)
        {
            ItemId = doc[_jsonId].AsInt;
            ItemName = doc[_jsonItemName].Value;
            Type = type;
        }

        private const string _jsonId = "id";
        private const string _jsonItemName = "itemName";
    }
}
