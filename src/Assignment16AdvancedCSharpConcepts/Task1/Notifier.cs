namespace Assignment16AdvancedCSharpConcepts.Task1
{
    /// <summary>
    /// Represents the class for notification service.
    /// </summary>
    internal class Notifier
    {
        /// <summary>
        /// Represents a delegate that handles notifications containing a message.
        /// </summary>
        /// <param name="message">The notification message.</param>
        public delegate void Notify(string message);

        /// <summary>
        /// Occurs when an action is performed, broadcasting a notification message to all subscribed listeners.
        /// </summary>
        public event Notify OnAction = (_) => { };

        /// <summary>
        /// Sends a notification message to all subscribed publishers.
        /// </summary>
        /// <param name="message">The notification message to send.</param>
        public void SendNotification(string message)
        {
            ConsoleHelpers.DisplayStatus($"Sending Notification to the publishers...");
            this.OnAction?.Invoke(message);
        }
    }
}
