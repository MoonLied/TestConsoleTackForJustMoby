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

        public NPCDict(JSONNode node) { 
            Id = node["id"].AsInt;
            LocationId = node["locid"].AsInt;
            NPCName = node["name"].Value;
            NPCDescription = node["desc"].Value;
        }

        public void AddQuest(QuestBase quest) {
            if (Quests == null) Quests = new List<QuestBase>();
            Quests.Add(quest);
        }
    }
}
