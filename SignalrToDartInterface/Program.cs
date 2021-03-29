﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SignalRToDartInterface.ExampleHub;

namespace SignalRToDartInterface {
    class Program {
        private static readonly GenerateRequest[] Requests = {
            new(typeof(ExampleHub.ExampleHub),
                new List<string> {"Return","OnConnectedAsync","OnDisconnectedAsync","Dispose"},
                new List<string>{ "Mapping" },
                isSignalRHub:true),
            new (typeof(HubResponses)),
        };

        static void Main(string[] args) {
            var result = ConvertSignalRToDart.Convert(Requests.ToList());
            Console.WriteLine(result);
            File.WriteAllText("signalRInterface.dart", result);
            Console.ReadLine();
        }
    }
}
