using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserController
{
    public class BorderlessTabControl : TabControl
    {
        public BorderlessTabControl()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (Parent != null)
            {
                using var bmp = new Bitmap(Parent.Width, Parent.Height);
                Parent.Controls.Cast<Control>()
                    .Where(c => c != this && c.Bounds.IntersectsWith(Bounds))
                    .ToList();
                var rect = new Rectangle(-Left, -Top, Parent.Width, Parent.Height);
                pevent.Graphics.TranslateTransform(-Left, -Top);
                var args = new PaintEventArgs(pevent.Graphics, rect);
                InvokePaintBackground(Parent, args);
                InvokePaint(Parent, args);
                pevent.Graphics.TranslateTransform(Left, Top);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < TabCount; i++)
            {
                DrawTabHeader(e.Graphics, i);
            }
        }

        /// <summary>
        /// 원래 탭 위치를 오른쪽으로 이동한 사각형 계산
        /// </summary>
        private Rectangle GetRightAlignedTabRect(int index)
        {
            var original = GetTabRect(index);

            // 전체 탭의 총 너비 계산
            int totalWidth = 0;
            for (int i = 0; i < TabCount; i++)
                totalWidth += GetTabRect(i).Width;

            // 오른쪽 끝에서 밀어낸 만큼 x 좌표 이동
            int shiftX = ClientRectangle.Right - totalWidth - 2;  // 2는 여백
            return new Rectangle(
                original.X + shiftX,
                original.Y,
                original.Width,
                original.Height);
        }

        private void DrawTabHeader(Graphics g, int index)
        {
            var rect = GetRightAlignedTabRect(index);
            bool selected = (index == SelectedIndex);

            Color backColor = selected
                ? Color.White
                : Color.FromArgb(240, 240, 240);
            Color foreColor = selected
                ? Color.FromArgb(0, 102, 204)
                : Color.FromArgb(80, 80, 80);

            using (var bg = new SolidBrush(backColor))
            {
                g.FillRectangle(bg, rect);
            }

            TextRenderer.DrawText(
                g, TabPages[index].Text, Font, rect, foreColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// 클릭한 좌표가 어느 탭에 해당하는지 직접 판정 (탭을 이동시켰으므로 필수)
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            for (int i = 0; i < TabCount; i++)
            {
                if (GetRightAlignedTabRect(i).Contains(e.Location))
                {
                    SelectedIndex = i;
                    return;
                }
            }
            base.OnMouseDown(e);
        }
    }
}
