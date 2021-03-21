using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TestForJustMoby.Dictionary;
using TestForJustMoby.Dictionary.ItemsModel;
using TestForJustMoby.Dictionary.Quest;

namespace TestForJustMoby
{
    public class GameEngine
    {
        public static GameEngine Instance { get; private set; }
        private static GameEngine _instance;
        public static void Initialize()
        {
            if (_instance == null) _instance = new GameEngine();
        }

        public static PlayerModel Player;
        public static GameStatus Status;
        private Dictionary<string, Action> _actionDict;

        public GameEngine() {
            Console.WriteLine("Запуск игрового движка");
            Player = new PlayerModel();
            _actionDict = new Dictionary<string, Action>();
            ActionCreator(GameStatus.Location);
        }

        public void ApplayAction(string action)
        {
            if (_actionDict.ContainsKey(action))
            {
                Console.Clear();
                _actionDict[action].Invoke();
            }
            else
            {
                Console.WriteLine("Неверная команда!");
                string str = Console.ReadLine();
                ApplayAction(str);
            }
        }

        #region Actions
        private void MoveToLocation(LocationDict loc)
        {
            Player.MoveToLocation(loc);
            ActionCreator(GameStatus.Location);
        }

        private void LookLocation(LocationDict loc) {
            ActionCreator(GameStatus.LookLocation);
        }

        private void NPCDialog(NPCDict npc)
        {
            ActionCreator(GameStatus.NPC, npc);
        }

        private void QuestCompleted(QuestBase quest) {
            Player.PlayerQuest.AddQuestComleted(quest);
            ActionCreator(GameStatus.Quest, null, quest);
        }

        private void ShowInventory() {
            Player.Inventory.ShowInventory();
            ActionCreator(GameStatus.Inventory);
        }
        #endregion

        #region Action Generator
        private void ActionCreator(GameStatus status, NPCDict npc = null, QuestBase quest = null)
        {
            Status = status;
            _actionDict.Clear();
            switch (status)
            {
                case GameStatus.Location:
                    ActionCreatorForLocation(Player.Location);
                    break;

                case GameStatus.NPC:
                    ActionCreatorForNPC(npc);
                    break;

                case GameStatus.Inventory:
                    ActionCreatorForBackLocation();
                    break;
                case GameStatus.Quest:
                    ActionCreatorForQuest(quest);
                    break;
                case GameStatus.LookLocation:
                    LocationDict loc = Player.Location;
                    Console.WriteLine($"{loc.LocName} ({loc.Id})");
                    Console.WriteLine($"Описание: {loc.LocDescription}");
                    ActionCreatorForBackLocation();
                    break;
            }

            string str = Console.ReadLine();
            ApplayAction(str);
        }
        private void ActionCreatorForLocation(LocationDict loc)
        {
            Console.WriteLine($"{loc.LocName} ({loc.Id})");
           
            Console.WriteLine($"Доступные действия:");

            if (loc.NPCInLocation != null)
            {
                for (int i = 0; i < loc.NPCInLocation.Count; i++)
                {
                    NPCDict npc = loc.NPCInLocation[i];
                    Console.WriteLine($"{_actionDict.Count + 1}. Поговорить с {npc.NPCName}");
                    Action action = new Action(() => NPCDialog(npc));
                    _actionDict[(_actionDict.Count + 1).ToString()] = action;
                }
            }

            ActionCreatorForLookLocation(Player.Location);

            for (int i = 0; i < loc.LocationsIdForPlayerMove.Count; i++) {
                LocationDict newLoc = DictionaryManager.Instance.LocationsDict[loc.LocationsIdForPlayerMove[i]];
                Console.WriteLine($"{_actionDict.Count+1}. Перейти в локацию {newLoc.LocName} ({newLoc.Id})");
                Action action = new Action(() =>MoveToLocation(newLoc));
                _actionDict[(_actionDict.Count + 1).ToString()] = action;
            }
           

            AddShowInventoryAction();
        }

        private void ActionCreatorForLookLocation(LocationDict loc) {
            Console.WriteLine($"{_actionDict.Count + 1}. Осмотреть окретности");
            Action action = new Action(() => LookLocation(loc));
            _actionDict[(_actionDict.Count + 1).ToString()] = action;
        }

        private void ActionCreatorForNPC(NPCDict npc)
        {
            bool noQuest = true;
            Console.WriteLine($"{npc.NPCName} ({npc.Id})");
            Console.WriteLine($"Описание: {npc.NPCDescription}");
            if (npc.Quests != null)
            {
                for (int i = 0; i < npc.Quests.Count; i++)
                {
                    QuestBase quest = npc.Quests[i];
                    if (!Player.PlayerQuest.СompletedQuests.Contains(quest))
                    {
                        Console.WriteLine($"У меня есть для тебя задание {quest.QuestName}");
                        Console.WriteLine($"Мне нужно {quest.QuestDescription}");
                    }
                }
                Console.WriteLine($"Доступные действия:");

                for (int i = 0; i < npc.Quests.Count; i++)
                {
                    QuestBase quest = npc.Quests[i];
                    if (!Player.PlayerQuest.СompletedQuests.Contains(quest))
                    {
                        Console.WriteLine($"{_actionDict.Count + 1}. Выполнить задание {quest.QuestName}");
                        Action action = new Action(() => QuestCompleted(quest));
                        _actionDict[(_actionDict.Count + 1).ToString()] = action;
                        noQuest = false;
                    }
                }
            }
            if (noQuest) Console.WriteLine($"У меня нет новых заданий для тебя ");

            ActionCreatorForBackLocation();
            AddShowInventoryAction();
        }

        private void ActionCreatorForBackLocation() {
            Console.WriteLine($"{_actionDict.Count + 1}. Вернуться в локацию");
            Action action = new Action(() => MoveToLocation(Player.Location));
            _actionDict[(_actionDict.Count + 1).ToString()] = action;
        }


        private void AddShowInventoryAction() {
            Console.WriteLine($"{_actionDict.Count + 1}. Открыть инвентарь");
            Action action = new Action(() => ShowInventory());
            _actionDict[(_actionDict.Count + 1).ToString()] = action;
        }

        private void ActionCreatorForQuest(QuestBase quest) {
            Console.WriteLine($"Вы выполнили задание {quest.QuestName}");
            NPCDict npc = DictionaryManager.Instance.NPCsDict[quest.NPCId];
            Console.WriteLine($"{npc.NPCName} передал вам награду:");
            QuestType type = quest.Type;

            switch (type)
            {
                case QuestType.testQuest:
                    TestQuest tQuest = (TestQuest)quest;

                    foreach (KeyValuePair<int, int> item in tQuest.QuestRewardItems) {
                        ItemBase itemB = DictionaryManager.Instance.ItemDict[item.Key];
                        Console.WriteLine($"{itemB.ItemName} - {item.Value} шт.");
                        Player.Inventory.AddItem(itemB,item.Value);
                    }
                    break;
            }
            ActionCreatorForBackLocation();
            AddShowInventoryAction();
        }
    }
    #endregion
}