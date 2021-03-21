using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary
{
    public class NPCDict
    {
        public int Id { get; private set; }
        public int LocationId { get; private set; }
        public string NPCName { get; private set; }
        public string NPCDescription { get; private set; }
        public List<QuestBase> Quests { get; private set; }

        public NPCDict(JSONNode node)
        {
            Id = node[_jsonId].AsInt;
            LocationId = node[_jsonLocId].AsInt;
            NPCName = node[_jsonName].Value;
            NPCDescription = node[_jsonDesc].Value;
        }

        public void AddQuest(QuestBase quest)
        {
            if (Quests == null) Quests = new List<QuestBase>();
            Quests.Add(quest);
        }

        private const string _jsonId = "id";
        private const string _jsonLocId = "locid";
        private const string _jsonName = "name";
        private const string _jsonDesc = "desc";
    }
}
