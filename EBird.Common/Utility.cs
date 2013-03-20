using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Globalization;

namespace EBird.Common
{
    public class EBirdUtility
    {
        private static Byte[] KEY_64 = System.Text.ASCIIEncoding.ASCII.GetBytes("27272727");
        private static Byte[] IV_64 = System.Text.ASCIIEncoding.ASCII.GetBytes("25252525");

        private long terabyte = 1099511627776;
        private long gigabyte = 1073741824;
        private long megabyte = 1048576;
        private long kilobyte = 1024;

        // returns DES encrypted string
        public static string Encrypt(string value)
        {
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);


            sw.Write(value);
            sw.Flush();
            cs.FlushFinalBlock();
            ms.Flush();

            // convert back to a string
            return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
        }

        // returns DES decrypted string
        public static string Decrypt(string value)
        {
            value = value.Replace(" ", "+");

            while (value.Length % 4 != 0)
            {
                value += "=";
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            Byte[] buffer = Convert.FromBase64String(value);
            MemoryStream ms = new MemoryStream(buffer);
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

        public string FormDBDate(DateTime inputString)
        {
            return inputString.ToString(Constant.DBDateFormat);
        }

        public string FormDBDate(DateTime inputString, string strFormat)
        {
            return inputString.ToString(strFormat);
        }

        public DateTime StringToDateTime(string dateStr, string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            return Convert.ToDateTime(dateStr, culture);
        }

        public ArrayList StringToArrayList(string value, char[] separator)
        {
            ArrayList _al = new ArrayList();
            //string[] _s = value.Split(new char[] { ',' });
            string[] _s = value.Split(separator);
            foreach (string item in _s)
                _al.Add(item);

            return _al;
        }

        public int getAspectRation(float OrgHeight, float OrgWidth, float InputHW, string requiredHW)
        {
            if (requiredHW == "H")
            {
                return Convert.ToInt32(OrgHeight / OrgWidth * InputHW);
            }
            else
            {
                return Convert.ToInt32(OrgWidth / OrgHeight * InputHW);
            }
        }

        public bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            //@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + 
            //@"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
            //@"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
            //@"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public bool isEmailsValid(string emailList, char[] separator)
        {
            ArrayList arry = StringToArrayList(emailList, separator);
            for (int idx = 0; idx < arry.Count; idx++)
            {
                if (!isEmail(arry[idx].ToString()))
                    return false;
            }
            return true;
        }

        public string getInvalidEmails(string emailList, char[] separator)
        {
            string strInvalid = string.Empty;

            ArrayList arry = StringToArrayList(emailList, separator);
            for (int idx = 0; idx < arry.Count; idx++)
            {
                if (!isEmail(arry[idx].ToString()))
                    strInvalid = strInvalid + arry[idx].ToString() + ",";
            }
            if (strInvalid.Length > 0)
                strInvalid = strInvalid.Substring(0, strInvalid.Length - 1);
            return strInvalid;
        }

        public string FormatFileSize(long bytes)
        {
            if (bytes > terabyte)
            {
                return ((float)bytes / (float)terabyte).ToString("0.00 TB");
            }
            else if (bytes > gigabyte)
            {
                return ((float)bytes / (float)gigabyte).ToString("0.00 GB");
            }
            else if (bytes > megabyte)
            {
                return (((float)bytes / (float)megabyte)).ToString("0.00 MB");
            }
            else if (bytes > kilobyte)
            {
                return ((float)bytes / (float)kilobyte).ToString("0.00 KB");
            }
            else return bytes + " Bytes";
        }

        public bool IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                {
                    Double.Parse(Expression.ToString());
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public bool IsInteger(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64)
                return true;
            try
            {
                if (Expression is string)
                {
                    int.Parse(Expression.ToString());
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public void RecordLogToFS(string logEntry)
        {
            string logText = DateTime.Now.ToLongDateString() + "                    ";

            string logFilename = Constant.DocRepRoot + "\\logs\\Applog" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + ".txt";
            File.AppendAllText(logFilename, logText.Substring(0, 20) + ": " + logEntry);
        }

        public bool isDateGreaterToday(DateTime dt)
        {
            try
            {
                TimeSpan ts = DateTime.Now.Subtract(dt);
                if (ts.Days < 0)
                    return true;
                else
                    return false;
            }
            catch
            {
            }
            return false;
        }

        public DateTime getWeekDayDate(int dtYear, int dtMonth, DayOfWeek dw, int occurOrder)
        {
            DateTime dt = new DateTime(dtYear, dtMonth, 1);
            bool isFound = false;
            int cntOccur = 1;
            while (!isFound)
            {
                if (dt.DayOfWeek == dw && occurOrder == cntOccur)
                    isFound = true;
                else
                    dt = dt.AddDays(1);
                ++cntOccur;
            }
            return dt;
        }

        public DateTime getlastWeekDayDate(int dtYear, int dtMonth, DayOfWeek dw)
        {
            DateTime dt = getLastDayDateOfMonth(dtYear, dtMonth);
            bool isFound = false;
            while (!isFound)
            {
                if (dt.DayOfWeek == dw)
                    isFound = true;
                else
                    dt = dt.AddDays(-1);
            }
            return dt;
        }

        public DateTime getLastDayDateOfMonth(int dtYear, int dtMonth)
        {
            DateTime dt = new DateTime(dtYear, dtMonth, 1);
            dt = (dt.AddMonths(1)).AddDays(-1);
            return dt;
        }

        public DayOfWeek getDayOftheWeek(int dayNo)
        {
            switch (dayNo)
            {
                case 0:
                    return DayOfWeek.Sunday;
                    break;
                case 1:
                    return DayOfWeek.Monday;
                    break;
                case 2:
                    return DayOfWeek.Tuesday;
                    break;
                case 3:
                    return DayOfWeek.Wednesday;
                    break;
                case 4:
                    return DayOfWeek.Thursday;
                    break;
                case 5:
                    return DayOfWeek.Friday;
                    break;
                case 6:
                    return DayOfWeek.Saturday;
                    break;
                default:
                    return DayOfWeek.Monday;
                    break;
            }
        }

        public DateTime getGMTDateTime(DateTime inputDate)
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            //Get date and time in US Mountain Standard Time 
            //dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            dateTime = TimeZoneInfo.ConvertTime(inputDate, timeZoneInfo);

            return dateTime;
            //return dateTime.ToString("yyyy-MM-dd HH-mm-ss");
        }
    }

    public class RandomPassword
    {
        // Define default min and max password lengths.
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

        // Define supported password characters divided into groups.
        // You can add (or remove) characters to (from) these groups.
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "23456789";
        private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        /// <remarks>
        /// The length of the generated password will be determined at
        /// random. It will be no shorter than the minimum default and
        /// no longer than maximum default.
        /// </remarks>
        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                            DEFAULT_MAX_PASSWORD_LENGTH);
        }

        /// <summary>
        /// Generates a random password of the exact length.
        /// </summary>
        /// <param name="length">
        /// Exact password length.
        /// </param>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        public static string Generate(int length)
        {
            return Generate(length, length);
        }

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <param name="minLength">
        /// Minimum password length.
        /// </param>
        /// <param name="maxLength">
        /// Maximum password length.
        /// </param>
        /// <returns>
        /// Randomly generated password.
        /// </returns>
        /// <remarks>
        /// The length of the generated password will be determined at
        /// random and it will fall with the range determined by the
        /// function parameters.
        /// </remarks>
        public static string Generate(int minLength,
                                      int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the password strength.
            char[][] charGroups = new char[][] 
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
        };

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string(password);
        }
        
    }

    public struct DateTimeWithZone
    {
        private readonly DateTime utcDateTime;
        private readonly TimeZoneInfo timeZone;

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
            this.timeZone = timeZone;
        }

        public DateTime UniversalTime { get { return utcDateTime; } }

        public TimeZoneInfo TimeZone { get { return timeZone; } }

        public DateTime LocalTime
        {
            get
            {
                return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
            }
        }
    }


}
