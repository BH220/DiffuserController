using DiffuserControllerNew.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DiffuserControllerNew.Api
{
    public static class HolidayApi
    {
        private static readonly HttpClient _http = new();

        public static async Task<List<HolidayItem>> GetHolidaysAsync(int year)
        {
            string url = $"https://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getRestDeInfo?serviceKey={App.ApiKey}&solYear={year}&numOfRows=500";

            string xml = await _http.GetStringAsync(url);

            var serializer = new XmlSerializer(typeof(HolidayResponse));
            using var reader = new StringReader(xml);
            var response = (HolidayResponse?)serializer.Deserialize(reader);

            if (response?.Header.ResultCode != "00")
                throw new Exception($"API 오류: {response?.Header.ResultMsg}");

            return response.Body.Items;
        }
    }
}
