using Autofac;
using Core;
using SQLite;
using System.Threading.Tasks;

namespace SampleApplication
{
    public interface IRepository
    {
        Task<Notification> DeleteContactAsync(Contact item);

        Task<FetchModelResult<Contact>> FetchContactAsync(string id);

        Task<FetchModelCollectionResult<Contact>> FetchContactsAsync();

        Task<HighriseUser> FetchHighriseUserAsync();

        Task Initialize();

        Task<Notification> SaveContactAsync(Contact item, ModelUpdateEvent updateEvent);

        Task<Notification> SaveHighriseUserAsyc(HighriseUser user);
    }

    public class Repository : IRepository
    {
        #region IRepository implementation

        private SQLiteAsyncConnection _database;

        private bool _isInitialized = false;

        public async Task<Notification> DeleteContactAsync(Contact item)
        {
            Notification retNotification = Notification.Success();
            try
            {
                await _database.DeleteAsync(item);
            }
            catch (SQLiteException)
            {
                //LOG:
                retNotification.Add(new NotificationItem("Save Failed"));
            }

            return retNotification;
        }

        public async Task<FetchModelResult<Contact>> FetchContactAsync(string id)
        {
            FetchModelResult<Contact> retResult = new FetchModelResult<Contact>();

            var item = await _database.FindAsync<Contact>(id);
            retResult.Model = item;

            return retResult;
        }

        public async Task<FetchModelCollectionResult<Contact>> FetchContactsAsync()
        {
            FetchModelCollectionResult<Contact> retResult = new FetchModelCollectionResult<Contact>();
            var items = await _database.Table<Contact>().ToListAsync();
            retResult.ModelCollection = items;
            return retResult;
        }

        public async Task<HighriseUser> FetchHighriseUserAsync()
        {
            var user = await _database.Table<HighriseUser>().FirstAsync();
            return user;
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
                await _database.CreateTableAsync<HighriseUser>();

                var userCount = await _database.Table<HighriseUser>().CountAsync();
                if (userCount == 0)
                {
                    var user = new HighriseUser
                    {
                        Name = "Inquisitor Jax",
                        Description = "Highrise user"
                    };
                    await _database.InsertAsync(user);
                }
            }
        }

        public async Task<Notification> SaveContactAsync(Contact item, ModelUpdateEvent updateEvent)
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

        public async Task<Notification> SaveHighriseUserAsyc(HighriseUser user)
        {
            Notification retNotification = Notification.Success();
            try
            {
                await _database.UpdateAsync(user);
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