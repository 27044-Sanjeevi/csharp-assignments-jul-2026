using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment16AdvancedCSharpConcepts.Task1
{
    internal class Notifier
    {
        public delegate void Notify(string message);

        public event Notify OnAction;

        public void SendNotification(string message)
        {
            ConsoleHelpers.DisplayStatus($"Sending Notification to the publishers...");
            this.OnAction?.Invoke(message);
        }
    }
}
