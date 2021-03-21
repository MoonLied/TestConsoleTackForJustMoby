using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary.ItemsModel
{
    public class ResouceDict : ItemBase
    {
        public ResouceDict(JSONNode doc) : base(doc)
        {
            Type = ItemType.Resource;
        }
    }
}
