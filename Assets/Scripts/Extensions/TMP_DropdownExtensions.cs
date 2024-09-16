using System;
using System.Diagnostics;
using TMPro;

public static class TMP_DropdownExtensions
{

    public static void SetToOption(this TMP_Dropdown dropdown, string optionName)
    {
        dropdown.value = dropdown.GetIndexForOptionName(optionName);
    }

    public static int GetIndexForOptionName(this TMP_Dropdown dropdown, string optionName)
    {
        if (string.IsNullOrEmpty(optionName))
        {
            return 0;
        }

        for(int x=0; x<dropdown.options.Count; x++)
        {
            if (dropdown.options[x].text.Equals(optionName))
            {
                return x;
            }
        }

        return 0;
    }

    public static void Clear(this TMP_Dropdown dropdown)
    {
        dropdown.value = 0;
    }

    public static bool HasNoSelectedItemFromBank(this TMP_Dropdown dropdown)
    {
        return StringConstants.DROPDOWN_UNSET.Equals(dropdown.GetSelectedItemName());
    }

    public static string GetSelectedItemName(this TMP_Dropdown dropdown)
    {
        return dropdown.options[dropdown.value].text;
    }

    public static void SetSelectedItemIndexFromBank(this TMP_Dropdown dropdown, string bankItemId)
    {
        dropdown.value = dropdown.GetSelectedItemIndexFromBank(bankItemId);
    }

    public static int GetSelectedItemIndexFromBank(this TMP_Dropdown dropdown, string bankItemId)
    {
        if (string.IsNullOrEmpty(bankItemId))
        {
            return 0;
        }

        try
        {
            for (int x=0; x<dropdown.options.Count; x++)
            {
                if (dropdown.options[x].text.Contains(bankItemId))
                {
                    return x;
                }
            }

            return 0;
        }
        catch
        {
            return 0;
        }
    }

    public static int GetSelectedItemIndexForBank(this TMP_Dropdown dropdown)
    {
        var selectedItem = dropdown.GetSelectedItemName();

        try
        {
            return int.Parse(selectedItem.Split(StringConstants.DROPDOWN_BANK_ITEM_CONCATINATOR)[0]);
        }
        catch 
        {
            return 0;
        }
    }

    public static T GetEnumValue<T>(this TMP_Dropdown dropdown) where T : Enum
    {

        return (T)Enum.Parse(typeof(T), dropdown.options[dropdown.value].text);
    }

}