using Autofac;
using Core;
using SQLite;
using System.Threading.Tasks;

namespace SampleApplication
{
    public interface IRepository
    {
        Task<FetchModelResult<Contact>> FetchSampleItemAsync(string id);

        Task<FetchModelCollectionResult<Contact>> FetchSampleItemsAsync();

        Task Initialize();

        Task<Notification> SaveSampleItemAsync(Contact item, ModelUpdateEvent updateEvent);
    }

    public class Repository : IRepository
    {
        #region IRepository implementation

        private SQLiteAsyncConnection _database;

        private bool _isInitialized = false;

        public async Task<FetchModelResult<Contact>> FetchSampleItemAsync(string id)
        {
            FetchModelResult<Contact> retResult = new FetchModelResult<Contact>();

            var item = await _database.FindAsync<Contact>(id);
            retResult.Model = item;

            return retResult;
        }

        public async Task<FetchModelCollectionResult<Contact>> FetchSampleItemsAsync()
        {
            FetchModelCollectionResult<Contact> retResult = new FetchModelCollectionResult<Contact>();
            var items = await _database.Table<Contact>().ToListAsync();
            retResult.ModelCollection = items;
            return retResult;
        }

        public async Task Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            var connectionFactory = CC.IoC.Resolve<IDatabaseConnectionFactory>();
            var connectionResult = connectionFactory.Execute(null);
            if (connectionResult.IsValid())
            {
                _database = connectionResult.Connection;
                await _database.CreateTableAsync<Contact>();
            }
        }

        public async Task<Notification> SaveSampleItemAsync(Contact item, ModelUpdateEvent updateEvent)
        {
            Notification retNotification = Notification.Success();
            try
            {
                if (updateEvent == ModelUpdateEvent.Created)
                {
                    await _database.InsertAsync(item);
                }
                else
                {
                    await _database.UpdateAsync(item);
                }
            }
            catch (SQLiteException)
            {
                //LOG:
                retNotification.Add(new NotificationItem("Save Failed"));
            }

            return retNotification;
        }

        #endregion IRepository implementation
    }
}