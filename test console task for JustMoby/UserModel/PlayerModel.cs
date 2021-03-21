using System;
using System.Collections.Generic;
using System.Text;
using test_console_task_for_JustMoby.Dictionary;
using test_console_task_for_JustMoby.UserModel;

namespace test_console_task_for_JustMoby
{
    public class PlayerModel
    {
        public LocationModel Location { get; private set; }
        public InventoryModel Inventory { get; private set; }
        public QuestModel PlayerQuest { get; private set; }

        public PlayerModel() {
            // хард код стартовой локации, можно добавить в словарь ИД стартовой локации
            Location = DictionaryManager.Instance.LocationsDict[1];
            Inventory = new InventoryModel();
            PlayerQuest = new QuestModel();
        }

        public void MoveToLocation(LocationModel loc) {
            Location = loc;
        }
    }
}
