// File generated at 3/24/2021 8:02:34 PM
import 'package:signalr_core/signalr_core.dart';

class ExampleHub {
  final HubConnection _connection;
  ExampleHub(
    this._connection,
  );

  void A_User(AddUser command) =>
      _connection.send(methodName: 'A_User', args: <Object>[command]);

  void U_User(UpdateUser command) =>
      _connection.send(methodName: 'U_User', args: <Object>[command]);

  void D_User(DeleteUser id) =>
      _connection.send(methodName: 'D_User', args: <Object>[id]);

  void G_User(int id) =>
      _connection.send(methodName: 'G_User', args: <Object>[id]);

  void GA_User() => _connection.send(methodName: 'GA_User', args: <Object>[]);

  void A_Account(AddAccount command) =>
      _connection.send(methodName: 'A_Account', args: <Object>[command]);

  void U__Account(UpdateAccount command) =>
      _connection.send(methodName: 'U__Account', args: <Object>[command]);

  void D__Account(DeleteAccount id) =>
      _connection.send(methodName: 'D__Account', args: <Object>[id]);

  void G_Account(GetAccount id) =>
      _connection.send(methodName: 'G_Account', args: <Object>[id]);

  void GA_Account() =>
      _connection.send(methodName: 'GA_Account', args: <Object>[]);
}

class AddUser {
  final String firstName;
  final String lastName;
  final int age;
  final List<Address> addresses;

  AddUser(this.firstName, this.lastName, this.age, this.addresses);

  AddUser.fromJson(Map<String, dynamic> json)
      : firstName = json['firstName'],
        lastName = json['lastName'],
        age = json['age'],
        addresses = json['addresses'] == null
            ? <Address>[]
            : List<Address>.from(
                json['addresses'].map((x) => Address.fromJson(x)));

  Map<String, dynamic> toJson() => {
        'firstName': firstName,
        'lastName': lastName,
        'age': age,
        'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
      };
}

class Address {
  final String city;
  final String street;
  final String number;

  Address(this.city, this.street, this.number);

  Address.fromJson(Map<String, dynamic> json)
      : city = json['city'],
        street = json['street'],
        number = json['number'];

  Map<String, dynamic> toJson() =>
      {'city': city, 'street': street, 'number': number};
}

class UpdateUser {
  final int id;
  final String firstName;
  final String lastName;
  final int age;
  final List<Address> addresses;

  UpdateUser(this.id, this.firstName, this.lastName, this.age, this.addresses);

  UpdateUser.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        firstName = json['firstName'],
        lastName = json['lastName'],
        age = json['age'],
        addresses = json['addresses'] == null
            ? <Address>[]
            : List<Address>.from(
                json['addresses'].map((x) => Address.fromJson(x)));

  Map<String, dynamic> toJson() => {
        'id': id,
        'firstName': firstName,
        'lastName': lastName,
        'age': age,
        'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
      };
}

class DeleteUser {
  final int id;

  DeleteUser(this.id);

  DeleteUser.fromJson(Map<String, dynamic> json) : id = json['id'];

  Map<String, dynamic> toJson() => {'id': id};
}

class AddAccount {
  final String userName;
  final String email;
  final String password;
  final List<String> rights;
  final DateTime lastVisit;

  AddAccount(
      this.userName, this.email, this.password, this.rights, this.lastVisit);

  AddAccount.fromJson(Map<String, dynamic> json)
      : userName = json['userName'],
        email = json['email'],
        password = json['password'],
        rights = json['rights'] == null
            ? <String>[]
            : List<String>.from(json['rights'].map((x) => x)),
        lastVisit = json['lastVisit'];

  Map<String, dynamic> toJson() => {
        'userName': userName,
        'email': email,
        'password': password,
        'rights': List<dynamic>.from(rights.map((x) => x)),
        'lastVisit': lastVisit
      };
}

class UpdateAccount {
  final int id;
  final String userName;
  final String email;
  final String password;
  final List<String> rights;
  final DateTime lastVisit;

  UpdateAccount(this.id, this.userName, this.email, this.password, this.rights,
      this.lastVisit);

  UpdateAccount.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        userName = json['userName'],
        email = json['email'],
        password = json['password'],
        rights = json['rights'] == null
            ? <String>[]
            : List<String>.from(json['rights'].map((x) => x)),
        lastVisit = json['lastVisit'];

  Map<String, dynamic> toJson() => {
        'id': id,
        'userName': userName,
        'email': email,
        'password': password,
        'rights': List<dynamic>.from(rights.map((x) => x)),
        'lastVisit': lastVisit
      };
}

class DeleteAccount {
  final int id;

  DeleteAccount(this.id);

  DeleteAccount.fromJson(Map<String, dynamic> json) : id = json['id'];

  Map<String, dynamic> toJson() => {'id': id};
}

class GetAccount {
  final int id;

  GetAccount(this.id);

  GetAccount.fromJson(Map<String, dynamic> json) : id = json['id'];

  Map<String, dynamic> toJson() => {'id': id};
}

enum HubResponses {
  R_User,
  R_Account,
}

class User {
  final int id;
  final String firstName;
  final String lastName;
  final int age;
  final List<Address> addresses;

  User(this.id, this.firstName, this.lastName, this.age, this.addresses);

  User.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        firstName = json['firstName'],
        lastName = json['lastName'],
        age = json['age'],
        addresses = json['addresses'] == null
            ? <Address>[]
            : List<Address>.from(
                json['addresses'].map((x) => Address.fromJson(x)));

  Map<String, dynamic> toJson() => {
        'id': id,
        'firstName': firstName,
        'lastName': lastName,
        'age': age,
        'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
      };
}

class Account {
  final String userName;
  final String email;
  final String password;
  final List<String> rights;
  final DateTime lastVisit;

  Account(
      this.userName, this.email, this.password, this.rights, this.lastVisit);

  Account.fromJson(Map<String, dynamic> json)
      : userName = json['userName'],
        email = json['email'],
        password = json['password'],
        rights = json['rights'] == null
            ? <String>[]
            : List<String>.from(json['rights'].map((x) => x)),
        lastVisit = json['lastVisit'];

  Map<String, dynamic> toJson() => {
        'userName': userName,
        'email': email,
        'password': password,
        'rights': List<dynamic>.from(rights.map((x) => x)),
        'lastVisit': lastVisit
      };
}
