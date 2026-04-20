using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiffuserController
{
    public class DateHelper
    {
        public BindingList<DateModel> Dates { get; set; }
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
                Dates = JsonSerializer.Deserialize<BindingList<DateModel>>(json);

            }
            else
            {
                Dates = new BindingList<DateModel>();
                Save();
            }
        }

        public void Save()
        {
            SortByDate();
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
        public void SortByDate()
        {
            if (Dates == null || Dates.Count <= 1) return;

            var sorted = Dates.OrderBy(x => x.Date).ToList();

            Dates.RaiseListChangedEvents = false;
            Dates.Clear();
            foreach (var item in sorted)
            {
                Dates.Add(item);
            }
            Dates.RaiseListChangedEvents = true;
            Dates.ResetBindings();
        }

    }
}
