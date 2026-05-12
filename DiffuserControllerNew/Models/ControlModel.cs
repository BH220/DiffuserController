using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Models
{
    public class ControlModel
    {
        // 선택된 USB의 ID
        public string SelectedUSB { get; set; }

        // 동작이 인터벌 형식인 경우 true
        public bool IsInterval { get; set; } = true;
        // 인터벌 방식의 시작시간
        public DateTime StartAt { get; set; } = new DateTime(2000, 1, 1, 9, 30, 0);
        // 인터벌 방식의 종료시간
        public DateTime EndAt { get; set; } = new DateTime(2000, 1, 1, 17, 0, 0);
        // 인터벌 방식의 간격 시간
        public DateTime Term { get; set; } = new DateTime(2000, 1, 1, 1, 0, 0);
        // 분사 유지 시간
        public int MaintainSecond { get; set; } = 1;

        // 동작이 스케줄 형식인 경우 true
        public bool IsSchedule { get; set; }
        // 스케줄 방식의 시간 목록(HH:mm 형식의 목록으로 저장)
        public List<string> ScheduleTimes { get; set; } = new List<string>();
        public bool IsRunning { get; set; } = true;
    }
}
