using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary.ItemsModel
{
    public class ResourceDict : ItemBase
    {
        public ResourceDict(JSONNode doc) : base(doc, ItemType.Resource)
        {
        }
    }
}
