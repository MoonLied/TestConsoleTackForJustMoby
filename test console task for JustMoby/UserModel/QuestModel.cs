using System;
using System.Collections.Generic;
using System.Text;
using TestForJustMoby.Dictionary;

namespace TestForJustMoby.UserModel
{
    public class QuestModel
    {
        public List<QuestBase> ActiveQuests { get; private set; }
        public List<QuestBase> СompletedQuests { get; private set; }

        public QuestModel()
        {
            ActiveQuests = new List<QuestBase>();
            СompletedQuests = new List<QuestBase>();
        }

        public void AddQuestComleted(QuestBase quest)
        {
            СompletedQuests.Add(quest);
        }
    }
}
