using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DiffuserController
{
    [XmlRoot("response")]
    public class HolidayResponse
    {
        [XmlElement("header")]
        public HolidayResponseHeader Header { get; set; } = new();

        [XmlElement("body")]
        public HolidayResponseBody Body { get; set; } = new();
    }

    public class HolidayResponseHeader
    {
        [XmlElement("resultCode")]
        public string ResultCode { get; set; } = "";

        [XmlElement("resultMsg")]
        public string ResultMsg { get; set; } = "";
    }

    public class HolidayResponseBody
    {
        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<HolidayItem> Items { get; set; } = new();

        [XmlElement("numOfRows")]
        public int NumOfRows { get; set; }

        [XmlElement("pageNo")]
        public int PageNo { get; set; }

        [XmlElement("totalCount")]
        public int TotalCount { get; set; }
    }

    public class HolidayItem
    {
        /// <summary>특일 종류 (01: 국경일/공휴일 등)</summary>
        [XmlElement("dateKind")]
        public string DateKind { get; set; } = "";

        /// <summary>특일 명칭 (예: 설날, 삼일절)</summary>
        [XmlElement("dateName")]
        public string DateName { get; set; } = "";

        /// <summary>공공기관 휴일 여부 (Y/N)</summary>
        [XmlElement("isHoliday")]
        public string IsHoliday { get; set; } = "";

        /// <summary>날짜 (yyyyMMdd)</summary>
        [XmlElement("locdate")]
        public int LocDate { get; set; }

        /// <summary>순번</summary>
        [XmlElement("seq")]
        public int Seq { get; set; }

        /// <summary>locdate(int)를 DateOnly로 변환</summary>
        [XmlIgnore]
        public DateOnly Date => DateOnly.ParseExact(LocDate.ToString(), "yyyyMMdd");

        /// <summary>휴일 여부 (bool)</summary>
        [XmlIgnore]
        public bool IsHolidayBool => IsHoliday == "Y";
    }
}
