using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary.ItemsModel
{
    public class AmmunitionDict : ItemBase
    {

        public AmmunitionDict(JSONNode doc) : base(doc)
        {
            Type = ItemType.Ammunition;
        }
    }
}
