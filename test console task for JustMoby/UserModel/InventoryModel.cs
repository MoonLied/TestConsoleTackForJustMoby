using System;
using System.Collections.Generic;
using System.Text;
using TestForJustMoby.Dictionary;
using TestForJustMoby.Dictionary.ItemsModel;

namespace TestForJustMoby.UserModel
{
    public class InventoryModel
    {
        public IReadOnlyDictionary<int, int> Resources => _resources;
        public IReadOnlyList<AmmunitionDict> Ammunition => _ammunition;

        private Dictionary<int, int> _resources; // словарь ресурсов где ключ = ИД итема, значение = кол-во
        private List<AmmunitionDict> _ammunition; // лист предметов, они отличаются своей логикой от ресурсов


        public InventoryModel()
        {
            _resources = new Dictionary<int, int>();
            _ammunition = new List<AmmunitionDict>();
        }

        public void AddItem(ItemBase item, int count)
        {
            switch (item.Type)
            {
                case ItemType.Resource:
                    if (_resources.ContainsKey(item.ItemId)) _resources[item.ItemId] += count;
                    else _resources[item.ItemId] = count;
                    break;
                case ItemType.Ammunition:
                    for (int i = 0; i < count; i++)
                    {
                        _ammunition.Add((AmmunitionDict)item);
                    }
                    break;
            }
        }

        public void ShowInventory()
        {
            StringBuilder strBilder = new StringBuilder();
            if (_ammunition.Count == 0 && _resources.Count == 0)
            {
                strBilder.Append("Ваш инвентарь пуст");
            }
            else
            {
                if (_ammunition.Count > 0)
                {
                    strBilder.Append("\n Аммуниция: ");
                    for (int i = 0; i < _ammunition.Count; i++)
                    {
                        strBilder.Append("\n " + _ammunition[i].ItemName);
                    }
                }
                if (_resources.Count > 0)
                {
                    Dictionary<int, ItemBase> resoucesDict = DictionaryManager.Instance.ItemDict;
                    strBilder.Append("\n Ресурсы: ");
                    foreach (KeyValuePair<int, int> item in _resources)
                    {
                        strBilder.Append("\n " + resoucesDict[item.Key].ItemName + " - " + item.Value.ToString() + " шт.");
                    }
                }
            }

            Console.WriteLine(strBilder.ToString());
        }

    }
}
