using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiffuserController
{
    public class DateHelper
    {
        public List<DateModel> Dates { get; private set; }
        private string Dic = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BH Soft", "Diffuser Controller");
        private string JsonPath
        {
            get
            {
                if (Directory.Exists(Dic) == false)
                    Directory.CreateDirectory(Dic);
                return Path.Combine(Dic, "data.json");
            }
        }
        private static DateHelper _instance = null;
        public static DateHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DateHelper();
                    _instance.Init();
                }
                return _instance;
            }
        }

        private void Init()
        {
            if (File.Exists(JsonPath))
            {
                string json = File.ReadAllText(JsonPath);
                Dates = JsonSerializer.Deserialize<List<DateModel>>(json);

            }
            else
            {
                Dates = new List<DateModel>();
                Save();
            }
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(Dates);
            File.Delete(JsonPath);
            File.WriteAllText(JsonPath, json);
        }

        public string GetMessage(DateTime selectionStart)
        {
            string result = "";
            
            var find = Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(selectionStart));
            if (find != null)
                result = find.Message;
            return result;
            
        }
    }
}
