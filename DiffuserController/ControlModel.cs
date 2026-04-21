using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserController
{
    public class ControlModel
    {
        // 선택된 USB의 ID
        public string SelectedUSB { get; set; }

        // 동작이 인터벌 형식인 경우 true
        public bool IsInterval { get; set; }
        // 인터벌 방식의 시작시간
        public DateTime StartAt { get; set; }
        // 인터벌 방식의 종료시간
        public DateTime EndAt { get; set; }
        // 인터벌 방식의 간격. 몇 초 마다 실행할 건지 
        public int IntervalSecond { get; set; }
        // 인터벌 방식의 분가 유지 시간. 몇 초 동안 분사 할건지
        public int IntervalMaintainSecond { get; set; }

        // 동작이 스케줄 형식인 경우 true
        public bool IsSchedule { get; set; }
        // 스케줄 방식의 시간 목록(HH:mm 형식의 목록으로 저장)
        public List<string> ScheduleTimes { get; set; }
        // 스케줄 방식의 분가 유지 시간. 몇 초 동안 분사 할건지
        public int ScheduleMaintainSecond { get; set; }
    }
}
