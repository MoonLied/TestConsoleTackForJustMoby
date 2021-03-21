using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary.Quest
{
    public class TestQuest : QuestBase
    {
        public Dictionary<int, int> QuestRewardItems { get; private set;}

        public TestQuest(JSONNode node): base(node)
        {
            Type = QuestType.testQuest;

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
