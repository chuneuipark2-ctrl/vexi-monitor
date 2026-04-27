using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VEXI
{
    public sealed class MouseWheelBlocker : NativeWindow, IDisposable
    {
        private readonly Control _source;

        public MouseWheelBlocker(Control source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));

            _source.HandleCreated += Source_HandleCreated;
            _source.HandleDestroyed += Source_HandleDestroyed;

            if (_source.IsHandleCreated)
                AssignHandle(_source.Handle);
        }

        private void Source_HandleCreated(object sender, EventArgs e) => AssignHandle(_source.Handle);
        private void Source_HandleDestroyed(object sender, EventArgs e) => ReleaseHandle();

        public void Dispose()
        {
            _source.HandleCreated -= Source_HandleCreated;
            _source.HandleDestroyed -= Source_HandleDestroyed;
            ReleaseHandle();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            const int WM_MOUSEHWHEEL = 0x020E; // 일부 장치/트랙패드

            if (m.Msg == WM_MOUSEWHEEL || m.Msg == WM_MOUSEHWHEEL)
            {
                // ✅ 여기서 먹어버림: 왼쪽에서는 휠로 스크롤이 절대 안 됨
                return;
            }

            base.WndProc(ref m);
        }
    }

    static class ListViewNative
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_GETTOPINDEX = LVM_FIRST + 39; // 0x1027

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static int GetTopIndex(ListView lv)
        {
            if (lv == null || !lv.IsHandleCreated) return 0;
            return (int)SendMessage(lv.Handle, LVM_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);
        }
    }

    public sealed class RightScrollToLeftTopSync : NativeWindow, IDisposable
    {
        private readonly ListView _right;
        private readonly ListView _left;
        private bool _syncing;

        public RightScrollToLeftTopSync(ListView right, ListView left)
        {
            _right = right ?? throw new ArgumentNullException(nameof(right));
            _left = left ?? throw new ArgumentNullException(nameof(left));

            _right.HandleCreated += (s, e) => AssignHandle(_right.Handle);
            _right.HandleDestroyed += (s, e) => ReleaseHandle();

            if (_right.IsHandleCreated) AssignHandle(_right.Handle);
        }

        public void Dispose() => ReleaseHandle();

        protected override void WndProc(ref Message m)
        {
            const int WM_VSCROLL = 0x0115;
            const int WM_MOUSEWHEEL = 0x020A;
            const int WM_KEYDOWN = 0x0100;

            bool mayScroll =
                (m.Msg == WM_VSCROLL) ||
                (m.Msg == WM_MOUSEWHEEL) ||
                (m.Msg == WM_KEYDOWN);

            // ✅ 먼저 오른쪽이 실제로 처리(스크롤)하게 둠
            base.WndProc(ref m);

            if (!mayScroll) return;
            if (_syncing) return;
            if (!_left.IsHandleCreated) return;
            if (_left.Items.Count == 0) return;

            _syncing = true;
            try
            {
                int top = ListViewNative.GetTopIndex(_right);
                if (top < 0) top = 0;
                if (top >= _left.Items.Count) top = _left.Items.Count - 1;

                _left.TopItem = _left.Items[top];
            }
            finally
            {
                _syncing = false;
            }
        }
    }

    public sealed class ListViewVScrollSync : IDisposable
    {
        private readonly ListView _src;   // 오른쪽
        private readonly ListView _dest;  // 왼쪽
        private readonly HookWindow _hook;
        private bool _inForward;

        public ListViewVScrollSync(ListView src, ListView dest)
        {
            _src = src;
            _dest = dest;

            _hook = new HookWindow(this);

            _src.HandleCreated += (s, e) => _hook.AssignHandle(_src.Handle);
            _src.HandleDestroyed += (s, e) => _hook.ReleaseHandle();

            if (_src.IsHandleCreated)
                _hook.AssignHandle(_src.Handle);
        }

        public void Dispose() => _hook.ReleaseHandle();

        private void HandleScroll(ref Message m)
        {
            if (_inForward) return;
            if (!_dest.IsHandleCreated) return;

            _inForward = true;
            try
            {
                const int WM_VSCROLL = 0x0115;

                if (m.Msg == WM_VSCROLL)
                {
                    int code = (m.WParam.ToInt32() & 0xFFFF);

                    // Thumb 드래그/놓기
                    const int SB_THUMBTRACK = 5;
                    const int SB_THUMBPOSITION = 4;

                    if (code == SB_THUMBTRACK || code == SB_THUMBPOSITION)
                    {
                        // ✅ 메시지 복제 대신 “결과(TopIndex)”를 맞춘다
                        int top = ListViewNative.GetTopIndex(_src);
                        if (_dest.Items.Count > 0)
                        {
                            if (top < 0) top = 0;
                            if (top >= _dest.Items.Count) top = _dest.Items.Count - 1;
                            _dest.TopItem = _dest.Items[top];
                        }
                        return;
                    }
                }

                // ✅ 휠/키보드/라인업다운 등은 기존대로 메시지 복제
                SendMessage(_dest.Handle, m.Msg, m.WParam, m.LParam);
            }
            finally
            {
                _inForward = false;
            }
        }

        private sealed class HookWindow : NativeWindow
        {
            private readonly ListViewVScrollSync _owner;
            public HookWindow(ListViewVScrollSync owner) { _owner = owner; }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                const int WM_VSCROLL = 0x0115;
                const int WM_MOUSEWHEEL = 0x020A;
                const int WM_KEYDOWN = 0x0100;

                if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL || m.Msg == WM_KEYDOWN)
                    _owner.HandleScroll(ref m);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}
