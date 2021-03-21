using System;
using System.Collections.Generic;
using System.Text;
using TestForJustMoby.Dictionary;
using TestForJustMoby.UserModel;

namespace TestForJustMoby
{
    public class PlayerModel
    {
        public LocationDict Location { get; private set; }
        public InventoryModel Inventory { get; private set; }
        public QuestModel PlayerQuest { get; private set; }

        public PlayerModel()
        {
            // хард код стартовой локации, можно добавить в словарь ИД стартовой локации
            Location = DictionaryManager.Instance.LocationsDict[1];
            Inventory = new InventoryModel();
            PlayerQuest = new QuestModel();
        }

        public void MoveToLocation(LocationDict loc)
        {
            Location = loc;
        }
    }
}
