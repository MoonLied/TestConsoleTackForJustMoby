using System;
using System.Collections.Generic;
using System.Text;

namespace test_console_task_for_JustMoby.Dictionary.ItemsModel
{
    public class ItemBase
    {
        public int ItemId { get; protected set; }
        public ItemType Type { get; protected set; }
        public string ItemName { get; protected set; }
    }
}
