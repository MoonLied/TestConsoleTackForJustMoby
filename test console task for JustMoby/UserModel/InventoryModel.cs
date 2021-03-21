using System;
using System.Collections.Generic;
using System.Text;
using test_console_task_for_JustMoby.Dictionary;
using test_console_task_for_JustMoby.Dictionary.ItemsModel;

namespace test_console_task_for_JustMoby.UserModel
{
    public class InventoryModel
    {
        public Dictionary<int, int> Resources { get; private set; } // словарь ресурсов где ключ = ИД итема, значение = кол-во
        public List<AmmunitionModel> Ammunition { get; private set; } // лист предметов, они отличаются своей логикой от ресурсов

        public InventoryModel() {
            Resources = new Dictionary<int, int>();
            Ammunition = new List<AmmunitionModel>();
        }

        public void AddItem(ItemBase item, int count) {
            switch (item.Type) {
                case ItemType.Resource:
                    if (Resources.ContainsKey(item.ItemId))  Resources[item.ItemId] += count;
                    else Resources[item.ItemId] = count;
                    break;
                case ItemType.Ammunition:
                    for (int i = 0; i < count; i++) {
                        Ammunition.Add((AmmunitionModel)item);
                    }
                    break;
            }
        }

        public void ShowInventory()
        {
            StringBuilder strBilder = new StringBuilder();
            if (Ammunition.Count == 0 && Resources.Count == 0)
            {
                strBilder.Append("Ваш инвентарь пуст");
            }
            else
            {
                if (Ammunition.Count > 0)
                {
                    strBilder.Append("\n Амуниция: ");
                    for (int i = 0; i < Ammunition.Count; i++)
                    {
                        strBilder.Append("\n " + Ammunition[i].ItemName);
                    }
                }
                if (Resources.Count > 0)
                {
                    Dictionary<int, ItemBase> resoucesDict = DictionaryManager.Instance.ItemDict; 
                     strBilder.Append("\n Ресурсы: ");
                    foreach(KeyValuePair<int,int> item in Resources)
                    {
                        strBilder.Append("\n "+resoucesDict[item.Key].ItemName+ " - " +item.Value.ToString() + " шт.");
                    }
                }
            }

            Console.WriteLine(strBilder.ToString());
        }

    }
}
