using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Api
{
    public class TestableUsersClient : IUsersClient
    {
        public int size = 0;
        public List<User> DeleteAsyncUsersList{get; set;}
        public int DeleteAsyncInvocationCount{get; set;}
        public Task<FileResponse> DeleteAsync(int id)
        {
            DeleteAsyncInvocationCount++;
            //DeleteAsyncUsersList.RemoveAt(id);
            return null;
        }
        public Task<FileResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAllUsersReturnValue { get; set; } = new();
        public int GetAllAsyncInvocationCount { get; set; }
        public Task<ICollection<User>?> GetAllAsync()
        {
            GetAllAsyncInvocationCount++;
            return Task.FromResult<ICollection<User>?>(GetAllUsersReturnValue);
        }

        public Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetAsync(int id)
        {
            return Task.FromResult<User>(GetAllUsersReturnValue[id]);
        }

        public Task<User> GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public int PostAsyncInvocationCount { get; set; }
        public List<User> PostAsyncInvokedParameters { get; } = new();
        public Task<User> PostAsync(User myUser)
        {
            size+=1;
            PostAsyncInvocationCount++;
            PostAsyncInvokedParameters.Add(myUser);
            return Task.FromResult(myUser);
        }

        public Task<User> PostAsync(User myUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<UpdateUser> PutAsyncInvocationParameters { get; } = new();
        public int PutAsyncInvocationCount { get; set; } = 0;
        public Task<FileResponse> PutAsync(int id, UpdateUser updatedUser)
        {
            PutAsyncInvocationCount++;
            PutAsyncInvocationParameters.Add(updatedUser);
            return null;
            throw new System.NotImplementedException();
        }

        public Task<FileResponse> PutAsync(int id, UpdateUser updatedUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}