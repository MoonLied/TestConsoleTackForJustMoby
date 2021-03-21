using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_console_task_for_JustMoby.Dictionary.Quest
{
    public class TestQuest : QuestBase
    {
        public Dictionary<int, int> QuestRewardItems { get; private set;}

        public TestQuest(JSONNode node, QuestType type)
        {
            Id = node["id"].AsInt;
            NPCId = node["npsid"].AsInt;
            QuestName = node["name"].Value;
            QuestDescription = node["desc"].Value;
            Type = type;
            if (node.ContainsKey("rewarditem")) // может и не быть, делаем проверку.
            {
                QuestRewardItems = new Dictionary<int, int>();
                foreach (JSONNode item in node["rewarditem"].AsArray) {
                    QuestRewardItems[item["id"].AsInt] = item["c"].AsInt;
                }
            }
        }
    }
}
