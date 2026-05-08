using DiffuserControllerNew.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Db
{
    [JsonObject]
    public class LocalDbManager
    {
        [JsonProperty]
        public BindingList<DateModel> Dates { get; set; }
        [JsonProperty]
        public ControlModel ControlModel { get; set; }

        public List<ComPortItem> Ports { get; set; }

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

        private static LocalDbManager _instance = null;
        public static LocalDbManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalDbManager();
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
                var a  = JsonConvert.DeserializeObject<LocalDbManager>(json);
                Dates = a.Dates;
                ControlModel = a.ControlModel;
            }
            else
            {
                Dates = new BindingList<DateModel>();
                ControlModel = new ControlModel();
                Save();
            }
        }

        public void Save()
        {
            SortByDate();
            string json = JsonConvert.SerializeObject(this);
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
