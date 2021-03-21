
using System.ComponentModel;

namespace TestForJustMoby
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
        Quest = 4,
        LookLocation = 5
    }
}
