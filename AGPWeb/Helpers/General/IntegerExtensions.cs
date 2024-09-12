namespace AGPWeb.Helpers.General
{

    public static class IntegerExtensions
    {
        public static string EncryptedString(this int valueToEncrypt)
        {
            return valueToEncrypt.ToString().Encrypt();
        }

        public static string EncryptedLongToString(this long valueToEncrypt)
        {
            return valueToEncrypt.ToString().Encrypt();
        }
    }
    public static class StringExtensions
    {
        public static string Encrypt(this string stringToEncrypt)
        {
            stringToEncrypt = Guid.NewGuid().ToString() + stringToEncrypt;
            byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(stringToEncrypt);
            return Convert.ToBase64String(encodedBytes);
        }

        public static string Decrypt(this string stringToDecrypt)
        {
            byte[] decodedBytes = Convert.FromBase64String(stringToDecrypt);
            return System.Text.Encoding.UTF8.GetString(decodedBytes).Remove(0, 36);
        }

        public static bool TryDecrypt(this string stringToDecrypt, out string decryptedString)
        {
            decryptedString = null;
            bool success = false;
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(stringToDecrypt);
                string str = System.Text.Encoding.UTF8.GetString(decodedBytes).Remove(0, 36);
                if (!string.IsNullOrEmpty(str))
                {
                    success = true;
                    decryptedString = str;
                }
            }
            catch (Exception) { }
            return success;
        }

        public static bool TryDecryptGUID(this string stringToDecrypt, out Guid decryptedGUID)
        {
            decryptedGUID = Guid.Empty;
            bool success = false;
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(stringToDecrypt);
                string str = System.Text.Encoding.UTF8.GetString(decodedBytes).Remove(0, 36);
                if (!string.IsNullOrEmpty(str))
                {
                    if (Guid.TryParse(str, out decryptedGUID))
                        success = true;
                }
            }
            catch (Exception) { }
            return success;
        }

        public static bool TryDecryptInteger(this string stringToDecrypt, out int decryptedInt)
        {
            decryptedInt = -1;
            bool success = false;
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(stringToDecrypt);
                string str = System.Text.Encoding.UTF8.GetString(decodedBytes).Remove(0, 36);
                if (!string.IsNullOrEmpty(str))
                {
                    if (int.TryParse(str, out decryptedInt) && decryptedInt != -1)
                        success = true;
                }
            }
            catch (Exception) { }

            return success;
        }

        public static bool TryDecryptLong(this string stringToDecrypt, out long decryptedLong)
        {
            decryptedLong = -1;
            bool success = false;
            try
            {
                byte[] decodedBytes = Convert.FromBase64String(stringToDecrypt);
                string str = System.Text.Encoding.UTF8.GetString(decodedBytes).Remove(0, 36);
                if (!string.IsNullOrEmpty(str))
                {
                    if (long.TryParse(str, out decryptedLong) && decryptedLong != -1)
                        success = true;
                }
            }
            catch (Exception) { }

            return success;
        }

        public static bool IsValidEmail(this string emailAddress)
        {
            bool isEmail = false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(emailAddress);
                isEmail = addr.Address == emailAddress;
            }
            catch (Exception) { }

            return isEmail;
        }

        public static string GetUnformattedNumber(this string formattedNumber)
        {
            return !string.IsNullOrEmpty(formattedNumber) ? new String(formattedNumber.Where(Char.IsDigit).ToArray()) : default(string);
        }

        public static string GetFormattedNumber(this string number, string format = "(###)-###-####")
        {
            var unformattedNum = number.GetUnformattedNumber();
            return !string.IsNullOrEmpty(unformattedNum) ? double.Parse(unformattedNum).ToString(format) : default(string);
        }
    }
}
