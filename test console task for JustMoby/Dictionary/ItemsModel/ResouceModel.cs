using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_console_task_for_JustMoby.Dictionary.ItemsModel
{
    public class ResouceModel : ItemBase
    {
        public ResouceModel(JSONNode doc, ItemType type)
        {
            ItemId = doc["id"].AsInt;
            Type = type;
            ItemName = doc["name"].Value;
        }
    }
}
