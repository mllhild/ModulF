using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ModulF.Tage
{
    internal class _08 : BaseClass
    {
        internal override void ResolveTasks()
        {
            ClassWithEvent classWithEvent = new ClassWithEvent();
            classWithEvent.valueHasChanged += ClassWithEvent_valueHasChanged;
        }

        private void ClassWithEvent_valueHasChanged(int oldVal, int newVal)
        {
            throw new NotImplementedException();
        }

        private static void ClassWithEvent_ValueHasChanged() { 
        }
    }

    internal delegate void valChanged(int oldVal , int newVal);
    internal delegate void valChanged2(object sender, int oldVal, int newVal);
    internal class ClassWithEvent
    {
        internal event valChanged valueHasChanged;
        protected System.Threading.Timer timer;
        protected int _value = 0;
        internal int value { get { return _value; } set { SetValue(value); } }

        internal ClassWithEvent()
        {
            timer.InitializeLifetimeService();
            timer.Change(0, 1000);
            //.......
        } 

        protected void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetValue(_value + 1);
        }

        protected void SetValue(int value)
        {
            int oldValue = _value;
            this.value = value;
        }

        internal void OnValueHasChanged(int oldValue, int newValue)
        {
            valueHasChanged?.Invoke(oldValue, newValue);
        }
    }
}
