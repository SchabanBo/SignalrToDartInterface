using System.Threading.Tasks;
using SignalRToDartInterface.ExampleHub.Types;

namespace SignalRToDartInterface.ExampleHub {
    public partial class ExampleHub /*** : Hub ***/ {
        internal readonly Database Database;

        public ExampleHub(Database database) {
            Database = database;
        }
        public async Task Return(string method, object response) {
        }

        public async Task Return(string method, object[] args) {
        }
    }

    /// <summary>
    /// User Hub Add Edit Delete Get Update
    /// </summary>
    public partial class ExampleHub /*** : Hub ***/ {
        public Task<string> A_User(AddUser command)
        {
            return Task.FromResult("Result");
        }

        public Task<HubResult> U_User(UpdateUser command)
        {
            return Task.FromResult(new HubResult());
        }

        public async Task D_User(DeleteUser id) {
            // Do work
        }

        public async Task G_User(int id) {
            // Do work
        }

        public async Task GA_User() {
            // Do work
        }
    }

    /// <summary>
    /// User Hub Add Edit Delete Get Update
    /// </summary>
    public partial class ExampleHub /*** : Hub ***/ {
        public async Task A_Account(AddAccount command) {
            // Do work
        }

        public async Task U__Account(UpdateAccount command) {
            // Do work
        }

        public async Task D__Account(DeleteAccount id) {
            // Do work
        }

        public async Task G_Account(GetAccount id) {
            // Do work
        }

        public async Task GA_Account() {
            // Do work
        }
    }

    public class Database { }
}
