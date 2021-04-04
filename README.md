# SignalrToDartInterface
Convert you c# Signalr class to dart class to use it with your projects using [signalr_core](https://pub.dev/packages/signalr_core)
Add the types you want to convert and call convert to get string that is the dart class
The package will convert the type and any other type that used in it.
```csharp
// Create you convert requests
 private static readonly GenerateRequest[] Requests = {
            new GenerateRequest(typeof(ExampleHub.ExampleHub),
                new List<string> {"Return","OnConnectedAsync","OnDisconnectedAsync","Dispose"}, // Names of methodes to not convert in this type
                new List<string>{ "Mapping" }, // Names of methodes to not convert in this type and its children
                isSignalRHub:true), // add signalr connection
            new GenerateRequest(typeof(HubResponses)),
            new GenerateRequest(typeof(User)),
            new GenerateRequest(typeof(Account)),
        };
// and send them
var result = ConvertSignalRToDart.Convert(Requests.ToList());
```
The result of this [ExampleHub](https://github.com/SchabanBo/SignalrToDartInterface/tree/master/SignalrToDartInterface/ExampleHub) is
```dart// File generated at 4/4/2021 2:40:37 PM
import 'package:signalr_core/signalr_core.dart';import 'package:flutter/widgets.dart';
class ExampleHub{
   final HubConnection connection;
   const ExampleHub({ @required this.connection});
   Future<String> A_User(AddUser command) async =>
       await connection.invoke('A_User' ,args: [command]) as String;

   Future<HubResult> U_User(UpdateUser command) async =>
      HubResult.fromJson( await connection.invoke('U_User' ,args: [command])as Map<String,dynamic>);

   void D_User(DeleteUser id) =>
      connection.send(methodName: 'D_User' ,args: [id]);

   void G_User(int id) =>
      connection.send(methodName: 'G_User' ,args: [id]);

   void GA_User() =>
      connection.send(methodName: 'GA_User');

   void A_Account(AddAccount command) =>
      connection.send(methodName: 'A_Account' ,args: [command]);

   void U__Account(UpdateAccount command) =>
      connection.send(methodName: 'U__Account' ,args: [command]);

   void D__Account(DeleteAccount id) =>
      connection.send(methodName: 'D__Account' ,args: [id]);

   void G_Account(GetAccount id) =>
      connection.send(methodName: 'G_Account' ,args: [id]);

   void GA_Account() =>
      connection.send(methodName: 'GA_Account');

}


class AddUser{
   final String firstName;
   final String lastName;
   final int age;
   final List<Address> addresses;
   const AddUser({this.firstName , this.lastName , this.age , this.addresses = const []});

   AddUser.fromJson(Map<String, dynamic> json):
       firstName = json['firstName'] as String,
       lastName = json['lastName'] as String,
       age = json['age'] as int,
       addresses =json['addresses'] == null ? <Address>[] : List<Address>.from(json['addresses'].map((x) =>Address.fromJson(x as Map<String, dynamic>)) as Iterable);

   Map<String, dynamic> toJson() => {
      'firstName': firstName,
      'lastName': lastName,
      'age': age,
      'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
   };
}


class Address{
   final String city;
   final String street;
   final String number;
   const Address({this.city , this.street , this.number });

   Address.fromJson(Map<String, dynamic> json):
       city = json['city'] as String,
       street = json['street'] as String,
       number = json['number'] as String;

   Map<String, dynamic> toJson() => {
      'city': city,
      'street': street,
      'number': number
   };
}


class HubResult{
   final Result result;
   final String message;
   const HubResult({this.result , this.message });

   HubResult.fromJson(Map<String, dynamic> json):
       result = Result.values[json['result'] as int],
       message = json['message'] as String;

   Map<String, dynamic> toJson() => {
      'result': result.index,
      'message': message
   };
}


enum Result{
   Done,
   Error,
}


class UpdateUser{
   final int id;
   final String firstName;
   final String lastName;
   final int age;
   final List<Address> addresses;
   const UpdateUser({this.id , this.firstName , this.lastName , this.age , this.addresses = const []});

   UpdateUser.fromJson(Map<String, dynamic> json):
       id = json['id'] as int,
       firstName = json['firstName'] as String,
       lastName = json['lastName'] as String,
       age = json['age'] as int,
       addresses =json['addresses'] == null ? <Address>[] : List<Address>.from(json['addresses'].map((x) =>Address.fromJson(x as Map<String, dynamic>)) as Iterable);

   Map<String, dynamic> toJson() => {
      'id': id,
      'firstName': firstName,
      'lastName': lastName,
      'age': age,
      'addresses': List<dynamic>.from(addresses.map((x) => x.toJson()))
   };
}


class DeleteUser{
   final int id;
   const DeleteUser({this.id });

   DeleteUser.fromJson(Map<String, dynamic> json):
       id = json['id'] as int;

   Map<String, dynamic> toJson() => {
      'id': id
   };
}


class AddAccount{
   final String userName;
   final String email;
   final String password;
   final List<String> rights;
   final DateTime lastVisit;
   const AddAccount({this.userName , this.email , this.password , this.rights = const [], this.lastVisit });

   AddAccount.fromJson(Map<String, dynamic> json):
       userName = json['userName'] as String,
       email = json['email'] as String,
       password = json['password'] as String,
       rights =json['rights'] == null ? <String>[] : List<String>.from(json['rights'].map((x) =>x) as Iterable),
       lastVisit = json['lastVisit'] as DateTime;

   Map<String, dynamic> toJson() => {
      'userName': userName,
      'email': email,
      'password': password,
      'rights': List<dynamic>.from(rights.map((x) =>x)),
      'lastVisit': lastVisit
   };
}


class UpdateAccount{
   final int id;
   final String userName;
   final String email;
   final String password;
   final List<String> rights;
   final DateTime lastVisit;
   const UpdateAccount({this.id , this.userName , this.email , this.password , this.rights = const [], this.lastVisit });

   UpdateAccount.fromJson(Map<String, dynamic> json):
       id = json['id'] as int,
       userName = json['userName'] as String,
       email = json['email'] as String,
       password = json['password'] as String,
       rights =json['rights'] == null ? <String>[] : List<String>.from(json['rights'].map((x) =>x) as Iterable),
       lastVisit = json['lastVisit'] as DateTime;

   Map<String, dynamic> toJson() => {
      'id': id,
      'userName': userName,
      'email': email,
      'password': password,
      'rights': List<dynamic>.from(rights.map((x) =>x)),
      'lastVisit': lastVisit
   };
}


class DeleteAccount{
   final int id;
   const DeleteAccount({this.id });

   DeleteAccount.fromJson(Map<String, dynamic> json):
       id = json['id'] as int;

   Map<String, dynamic> toJson() => {
      'id': id
   };
}


class GetAccount{
   final int id;
   const GetAccount({this.id });

   GetAccount.fromJson(Map<String, dynamic> json):
       id = json['id'] as int;

   Map<String, dynamic> toJson() => {
      'id': id
   };
}


class Account{
   final String userName;
   final String email;
   final String password;
   final List<String> rights;
   final DateTime lastVisit;
   final AccountType accountType;
   const Account({this.userName , this.email , this.password , this.rights = const [], this.lastVisit , this.accountType });

   Account.fromJson(Map<String, dynamic> json):
       userName = json['userName'] as String,
       email = json['email'] as String,
       password = json['password'] as String,
       rights =json['rights'] == null ? <String>[] : List<String>.from(json['rights'].map((x) =>x) as Iterable),
       lastVisit = json['lastVisit'] as DateTime,
       accountType = AccountType.values[json['accountType'] as int];

   Map<String, dynamic> toJson() => {
      'userName': userName,
      'email': email,
      'password': password,
      'rights': List<dynamic>.from(rights.map((x) =>x)),
      'lastVisit': lastVisit,
      'accountType': accountType.index
   };
}


enum AccountType{
   CheckIn,
   CheckOut,
}


enum HubResponses{
   R_User,
   R_Account,
}

```
