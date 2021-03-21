using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary.Quest
{
    public class TestQuest : QuestBase
    {
        public Dictionary<int, int> QuestRewardItems { get; private set; }

        public TestQuest(JSONNode node) : base(node, QuestType.testQuest)
        {
            if (node.ContainsKey(_jsonRewardItem)) // может и не быть, делаем проверку.
            {
                QuestRewardItems = new Dictionary<int, int>();
                foreach (JSONNode item in node[_jsonRewardItem].AsArray)
                {
                    QuestRewardItems[item[_jsonItemId].AsInt] = item[_jsonItemCount].AsInt;
                }
            }
        }

        private const string _jsonRewardItem = "rewarditem";
        private const string _jsonItemId = "id";
        private const string _jsonItemCount = "c";
    }
}
