﻿using SimpleJson;
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

            if (node.ContainsKey(_jsonRewardItem)) // может и не быть, делаем проверку.
            {
                QuestRewardItems = new Dictionary<int, int>();
                foreach (JSONNode item in node[_jsonRewardItem].AsArray) {
                    QuestRewardItems[item["id"].AsInt] = item["c"].AsInt;
                }
            }
        }

        private const string _jsonRewardItem = "rewarditem";
    }
}
