
using System.ComponentModel;

namespace test_console_task_for_JustMoby
{
    public enum QuestType
    {
        [Description("testQuest")] testQuest = 1
    }

    public enum ItemType
    {
        [Description("Resource")] Resource = 1,
        [Description("Ammunition")] Ammunition = 2
    }

    public enum GameStatus
    {
        Location = 1,
        Inventory = 2,
        NPC = 3,
        Quest = 4
    }
}
