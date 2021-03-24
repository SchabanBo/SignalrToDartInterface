using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SignalRToDartInterface {
    public class GenerateRequest {
        public Type Type { get; set; }
        public List<string> SkipMethodsToChildren { get; }
        public bool IsSignalRHub { get; set; }

        public List<MethodInfo> Methods { get; }
        public List<PropertyInfo> Properties { get; }

        public GenerateRequest(Type type , List<string> skipMethods = null, List<string> skipMethodsToChildren = null, List<string> skipProperties =null, bool isSignalRHub = false) {
            Type = type;
            SkipMethodsToChildren = skipMethodsToChildren ?? new List<string>();
            IsSignalRHub = isSignalRHub;
            Methods = Type.GetMethods().ToList();
            Properties = Type.GetProperties().ToList();
            skipMethods ??= new List<string>();
            skipProperties ??= new List<string>();
            if (IsSignalRHub) {
                skipProperties.Add("Clients");
                skipProperties.Add("Context");
                skipProperties.Add("Groups");
            }
            skipMethods.AddRange(new[] { "Equals", "ToString", "GetHashCode", "GetType" }); 
            var propertiesGetters = Properties.Select(p => $"get_{p.Name}").ToArray();
            var propertiesSetters = Properties.Select(p => $"set_{p.Name}").ToArray();
            Properties.RemoveAll(p => skipProperties.Contains(p.Name));
            Methods.RemoveAll(m => skipMethods.Contains(m.Name));
            Methods.RemoveAll(m => propertiesGetters.Contains(m.Name));
            Methods.RemoveAll(m => propertiesSetters.Contains(m.Name));
            Methods.RemoveAll(m => SkipMethodsToChildren.Contains(m.Name));
        }
    }
}
