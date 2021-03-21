using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary
{
    public class QuestBase
    {
        //Скорее всего от этого класса будет наследование. 
        //Т.к. квесты могут быть разные по своему типу (приниси/отнеси/убей/поговори)
        // в рамках тестового задания будет один тестовый квест
        public int Id { get; protected set; }
        public int NPCId { get; protected set; }
        public string QuestName { get; protected set; }
        public string QuestDescription { get; protected set; }
        public QuestType Type { get; protected set; }

        // в рамках тестового награда только предметы.
        // могут быть созданы и другие поля (опыт/уровень/репутация и т.д)
        //так же можно расширить до требований. 
        //минимайльный уровень / репутация игрока / выполненый квест цепочки и т.д. для получения квеста
        //Условия (количественные) 
        //но это уже больше в наследовании.

        public QuestBase(JSONNode node) {
            Id = node["id"].AsInt;
            NPCId = node["npsid"].AsInt;
            QuestName = node["name"].Value;
            QuestDescription = node["desc"].Value;
        }

        
    }
}
