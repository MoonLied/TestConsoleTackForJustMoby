using SimpleJson;
using System;
using System.Collections.Generic;
using System.IO;
using TestForJustMoby.Dictionary.ItemsModel;
using TestForJustMoby.Dictionary.Quest;
using static System.Net.Mime.MediaTypeNames;

namespace TestForJustMoby.Dictionary
{
    public class DictionaryManager
    {
        public static DictionaryManager Instance => _instance;
        private static DictionaryManager _instance;

        public static void Initialize()
        {
            if (_instance == null) _instance = new DictionaryManager();
        }

        private const string CollectionLocations = "locations";
        private const string CollectionNPCs = "npcs";
        private const string CollectionQuests = "quests";
        private const string CollectionItems = "items";

        public Dictionary<int, LocationDict> LocationsDict { get; private set; }
        public Dictionary<int, NPCDict> NPCsDict { get; private set; }
        public Dictionary<int, QuestBase> QuestsDict { get; private set; }
        public Dictionary<int, ItemBase> ItemDict { get; private set; }

        public DictionaryManager()
        {
            Console.WriteLine("Загрузка игрового словоря");
            string srtDict = File.ReadAllText("Dictionary/GameDict.json");
            JSONNode node = JSON.Parse(srtDict);

            LocationsLoad(node[CollectionLocations]);
            NPCsLoad(node[CollectionNPCs]);
            QuestsLoad(node[CollectionQuests]);
            ItemsLoad(node[CollectionItems]);
            Console.WriteLine("Игровой словарь загружен");
        }

        private void LocationsLoad(JSONNode node)
        {
            LocationsDict = new Dictionary<int, LocationDict>();
            foreach (JSONNode doc in node.AsArray)
            {
                LocationDict loc = new LocationDict(doc);
                LocationsDict[loc.Id] = loc;
            }
        }
        private void NPCsLoad(JSONNode node)
        {
            NPCsDict = new Dictionary<int, NPCDict>();
            foreach (JSONNode doc in node.AsArray)
            {
                NPCDict npc = new NPCDict(doc);
                NPCsDict[npc.Id] = npc;
                LocationsDict[npc.LocationId].AddNPC(npc);
            }
        }

        private void QuestsLoad(JSONNode node)
        {
            QuestsDict = new Dictionary<int, QuestBase>();
            foreach (JSONNode doc in node.AsArray)
            {
                QuestType type = doc["type"].Value.ToEnumVal<QuestType>();
                QuestBase quest = null;
                switch (type)
                {
                    case QuestType.testQuest:
                        quest = new TestQuest(doc);
                        break;
                }

                if (quest == null) continue;
                QuestsDict[quest.Id] = quest;
                NPCsDict[quest.NPCId].AddQuest(quest);
            }
        }

        private void ItemsLoad(JSONNode node)
        {
            ItemDict = new Dictionary<int, ItemBase>();
            foreach (JSONNode doc in node.AsArray)
            {
                ItemType type = doc["type"].Value.ToEnumVal<ItemType>();
                ItemBase item = null;
                switch (type)
                {
                    case ItemType.Resource:
                        item = new ResourceDict(doc);
                        break;
                    case ItemType.Ammunition:
                        item = new AmmunitionDict(doc);
                        break;
                }
                if (item != null) ItemDict[item.ItemId] = item;
            }
        }

    }
}
