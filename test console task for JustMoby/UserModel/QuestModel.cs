using System;
using System.Collections.Generic;
using System.Text;
using test_console_task_for_JustMoby.Dictionary;

namespace test_console_task_for_JustMoby.UserModel
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

        public void AddQuestComleted(QuestBase quest) {
            СompletedQuests.Add(quest);
        }
    }
}
