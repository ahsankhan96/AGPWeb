using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using AGPWeb.Models.DB;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace AGPWeb.Helpers.General
{
    public class Common
    {

        DbContextOptions<DBContext> dbOption;
        private static IWebHostEnvironment _env;

        public Common(IWebHostEnvironment environment)
        {
            _env = environment;
            string Basepath = _env.WebRootPath;
            string Contentpath = _env.ContentRootPath;
        }


        public static string FormatRelativeTime(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
            {
                return $"{Math.Floor(timeSpan.TotalSeconds)} seconds ago";
            }
            else if (timeSpan.TotalMinutes < 60)
            {
                return $"{Math.Floor(timeSpan.TotalMinutes)} minutes ago";
            }
            else if (timeSpan.TotalHours < 24)
            {
                return $"{Math.Floor(timeSpan.TotalHours)} hours ago";
            }
            else if (timeSpan.TotalDays < 7)
            {
                return $"{Math.Floor(timeSpan.TotalDays)} days ago";
            }
            else if (timeSpan.TotalDays < 30)
            {
                return $"{Math.Floor(timeSpan.TotalDays / 7)} weeks ago";
            }
            else if (timeSpan.TotalDays < 365)
            {
                return $"{Math.Floor(timeSpan.TotalDays / 30)} months ago";
            }
            else
            {
                return $"{Math.Floor(timeSpan.TotalDays / 365)} years ago";
            }
        }
        public static string ConvertToAbbreviation(long number)
        {
            if (number >= 1000000)
            {
                return (number / 1000000.0).ToString("0.#") + "m";
            }
            else if (number >= 1000)
            {
                return (number / 1000.0).ToString("0.#") + "k";
            }
            else
            {
                return number.ToString();
            }
        }
        public static string Decrypt(string input, string key)
        {
            try
            {
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        public static string Serialize(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.None });
            return result;
        }

        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static T Deserialize<T>(string json, IsoDateTimeConverter dateformat)
        {
            return JsonConvert.DeserializeObject<T>(json, dateformat);
        }
        public static List<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d)).ToList();
        }

        public static string SaveImage(string ImgStr)
        {
            try
            {
                if (String.IsNullOrEmpty(ImgStr)) { return null; };

                //String path = "~/img/upload";
                //String p = HttpContext.Current.Server.MapPath("/img/upload"); //Path
                string imageName = Guid.NewGuid().ToString() + ".png";
                String path = Path.Combine(_env.WebRootPath, "img", "upload", imageName);



                byte[] imageBytes = Convert.FromBase64String(ImgStr);

                File.WriteAllBytes(path, imageBytes);

                return imageName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetMD5Hash(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
                return plaintext;
            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
            byte[] hasedvalue = MD5provider.ComputeHash(Encoding.Default.GetBytes(plaintext));
            StringBuilder str = new StringBuilder();
            for (int counter = 0; counter < hasedvalue.Length; counter++)
            {
                str.Append(hasedvalue[counter].ToString("x2"));
            }
            return str.ToString();
        }

        public static string GetName(string title)
        {
            return title.Trim().Replace("  ", "").Replace(" ", "-").Replace("/", "-").Replace("&", "").Replace("--", "").ToLower();
        }

        public static bool IsCreditCardInfoValid(string cardNo)
        {
            cardNo = cardNo.Replace(" ", "").Replace("-", "");

            // Check if the card number contains only digits and has a valid length (typically between 13 and 19 digits)
            if (!System.Text.RegularExpressions.Regex.IsMatch(cardNo, @"^\d{13,19}$"))
            {
                return false;
            }

            // Convert the card number to an array of integers
            int[] digits = cardNo.Select(c => int.Parse(c.ToString())).ToArray();

            // Double every second digit from the right, starting from the second to last digit
            for (int i = digits.Length - 2; i >= 0; i -= 2)
            {
                digits[i] *= 2;
                if (digits[i] > 9)
                {
                    digits[i] -= 9;
                }
            }

            // Sum up all the digits
            int sum = digits.Sum();

            // If the sum is a multiple of 10, the card number is valid
            return sum % 10 == 0;

        }
        public static void sendemailForPix(string tos, string Subject, string content)
        {

            //   var _url = Constants.url;
            Task mytask = Task.Run(() =>
            {
                MailAddress fromAddress = new MailAddress("it.exp3rt@gmail.com");

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(fromAddress.Address, "Pixcile Technologies");
                // foreach (var to in tos)
                //   {
                //     if (!string.IsNullOrEmpty(to))
                mail.To.Add(new MailAddress(tos));
                //  }

                mail.Subject = Subject;
                //   string baseurl = _url;
                // content = content.Replace("@{{baseurl}}", baseurl);
                mail.Body = content;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com");

                client.Port = 587;
                client.UseDefaultCredentials = false;

                client.Credentials = new NetworkCredential("it.exp3rt@gmail.com", "agohdcroykulqsve");
                client.EnableSsl = true;

                //   client.Timeout = 28900000;
                try
                {
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                }

                catch (Exception ex)
                {
                    //Logger.LogWriterException(ex);
                }
            });
        }
        public static void SendResetCodeEmail(string email, string resetlink)
        {
            try
            {
                sendemailForPix(email, "Reset Password", resetlink);
            }
            catch (Exception ex)
            {
                // Log exception or handle errors
                Console.WriteLine(ex.Message);
            }
        }
        public static string Base64ToFile(string basePath, string base64, string extension)
        {
            try
            {
                if (string.IsNullOrEmpty(base64)) { return null; };

                if (base64.Contains("base64,"))
                {
                    base64 = base64.Split("base64,")[1];
                }

                string filename = Guid.NewGuid().ToString() + extension;

                var folder = Path.Combine(basePath, "img", "upload", DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = Path.Combine(folder, filename);

                byte[] audioBytes = Convert.FromBase64String(base64);

                File.WriteAllBytes(path, audioBytes);

                return DateTime.Now.ToString("yyyyMMdd") + "/" + filename;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<string> SaveFileAsync(string basePath, IFormFile file, string extension)
        {
            try
            {
                if (file == null || file.Length == 0) { return null; }

                string filename = Guid.NewGuid().ToString() + extension;
                var folder = Path.Combine(basePath, "img", "upload", DateTime.Now.ToString("yyyyMMdd"));

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = Path.Combine(folder, filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return DateTime.Now.ToString("yyyyMMdd") + "/" + filename;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }

}
