namespace ReGaSLZR
{

    using TMPro;

    public static class TMP_InputFieldExtensions
    {

        public static void Clear(this TMP_InputField input)
        {
            if (input.contentType == TMP_InputField.ContentType.IntegerNumber
                || input.contentType == TMP_InputField.ContentType.DecimalNumber)
            {
                input.text = "0";
            }
            else
            {
                input.text = string.Empty;
            }
        }

    }

}