using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_console_task_for_JustMoby.Dictionary
{
    public class NPCModel
    {
        public int Id { get; private set; }
        public int LocationId { get; private set; }
        public string NPCName { get; private set; }
        public string NPCDescription { get; private set; }
        public List<QuestBase> Quests { get; private set; }

        public NPCModel(JSONNode node) { 
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
