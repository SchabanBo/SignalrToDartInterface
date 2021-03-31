using System;
using System.Collections.Generic;

namespace SignalRToDartInterface.ExampleHub.Types {
    public class Account {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Rights { get; set; }
        public DateTime LastVisit { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class AddAccount {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Rights { get; set; }
        public DateTime LastVisit { get; set; }
    }

    public class UpdateAccount {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Rights { get; set; }
        public DateTime LastVisit { get; set; }
    }

    public class DeleteAccount {
        public int Id { get; set; }
    }

    public class GetAccount {
        public int Id { get; set; }
    }

    public enum AccountType {
        CheckIn,
        CheckOut,
    }
}
