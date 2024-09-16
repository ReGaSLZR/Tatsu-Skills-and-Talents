namespace ReGaSLZR
{

    using System.Collections.Generic;
    using TMPro;
    using UnityEditor;
    using UnityEngine;

    public abstract class AbstractBank<T> : ScriptableObject where T : class
    {

        [SerializeField]
        protected List<T> items;

        public List<T> Items => items;

        public T GetSelectedItemFromDropdown(TMP_Dropdown dropdown)
        {
            if (dropdown == null || items.Count == 0 || dropdown.HasNoSelectedItemFromBank())
            {
                return default;
            }

            var index = dropdown.GetSelectedItemIndexForBank();

            return items[index];
        }

        public abstract string GetItemId(T item);

        public void SaveItem(T item)
        {
            Debug.Log($"{GetType().Name}.SaveItem() called for item ID: {GetItemId(item)}");

            foreach (var storedItem in items)
            {
                if (string.Equals(GetItemId(storedItem), GetItemId(item)))
                {
                    items.Remove(storedItem);
                    items.Add(item);
                    Debug.Log($"{GetType().Name}.SaveItem() Overwrote existing item");
                    return;
                }
            }

            Debug.Log($"{GetType().Name}.SaveItem() storing as new item");
            items.Add(item);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }

    }

}