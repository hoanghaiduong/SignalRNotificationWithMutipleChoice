using myapp.Hubs;
using myapp.Interfaces;
using myapp.Models;
using TableDependency.SqlClient;

namespace myapp.SubscribeTableDependency
{
    public class SubscribeNotificationTableDependencies : ISubscribeTableDependency
    {
        SqlTableDependency<Notification> _tblDependency;
        NotificationHub _hub;
        private readonly ILogger<SubscribeNotificationTableDependencies> _logger;
        public SubscribeNotificationTableDependencies(NotificationHub hub, ILogger<SubscribeNotificationTableDependencies> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            try
            {
                   if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));
            }
            _tblDependency = new SqlTableDependency<Notification>(connectionString);
            _tblDependency.OnChanged += _tblDependency_OnChanged;
            _tblDependency.OnError += _tblDependency_OnError;
            _tblDependency.Start();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        private void _tblDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            try
            {
                Console.WriteLine($"{nameof(Notification)} SqlTableDependency Error : {e.Error.Message}");
                _logger.LogError($"{nameof(Notification)} SqlTableDependency Error : {e.Error.Message}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                _logger.LogError($"{ex.Message}");
            }
        }

        private async void _tblDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            try
            {
                if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
                {

                    var notification = e.Entity;
                    if (notification == null)
                    {
                        _logger.LogWarning("Received a null notification entity.");
                        return;
                    }

                    if (notification.MessageType == "ALL")
                    {
                        await _hub.SendNotificationToAll(notification.Message);
                    }
                    else if (notification.MessageType == "Personal")
                    {
                        await _hub.SendNotificationToClient(notification.Username, notification.Message);
                    }
                    else if (notification.MessageType == "Group")
                    {
                        await _hub.SendNotificationToGroup(notification.Username, notification.Message);
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
