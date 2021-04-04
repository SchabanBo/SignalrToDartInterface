using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SignalRToDartInterface {
    public class InterfaceGenerator {
        private readonly GenerateRequest _request;

        public static List<string> StandardTypes = new() {
            "DateTime",
            "String",
            "int",
            "double",
            "void",
            "Object",
        };

        public static List<Type> KnownType = new() {
            typeof(DateTime),
            typeof(string),
            typeof(int),
            typeof(double),
            typeof(void),
            typeof(object),
            typeof(char),
            typeof(Task),
            typeof(Task<>),
            typeof(Exception),
            typeof(Type),
            typeof(DateTime[]),
            typeof(string[]),
            typeof(int[]),
            typeof(double[]),
            typeof(object[]),
            typeof(char[]),
            typeof(Task[]),
            typeof(Exception[]),
            typeof(TypeCode),
            typeof(Int32)
        };

        private readonly List<Type> _typesToGenerate = new();

        public InterfaceGenerator(GenerateRequest request) {
            _request = request;
            KnownType.Add(request.Type);
        }

        public string Generate() {
            var result = "";
            if (_request.IsSignalRHub) {
                result += $"// File generated at {DateTime.Now}";
                result += result.AddNewLine();
                result += "import 'package:signalr_core/signalr_core.dart';";
                result += "import 'package:flutter/widgets.dart';";
                result += result.AddNewLine();
            }
            result += $"class {_request.Type.Name}";
            result += "{";
            result += result.AddNewLine();
            result += GetConstructor();
            result += result.AddNewLine();
            result += string.Join("", _request.Methods.Select(GetMethod));
            if (_request.Properties.Any()) {
                result += result.AddNewLine();
                result += GetJsonParser();
            }
            result += "}";
            foreach (var type in _typesToGenerate) {
                result += result.AddNewLine(3);
                result += type.IsEnum ? new EnumGenerator(type).Generate() : new InterfaceGenerator(new GenerateRequest(type, skipMethodsToChildren: _request.SkipMethodsToChildren)).Generate();
            }
            return result;
        }

        private string GetConstructor() {
            var result = "";

            if (!_request.IsSignalRHub && !_request.Properties.Any()) {
                return result;
            }

            var props = _request.Properties.Select(p => {
                var type = GetTypeName(p.PropertyType);
                var isNullable = type.Contains("?");
                var required = StandardTypes.Contains(type) && !isNullable && type != "String" && _request.IsNullSafety; // Set string always to nullable
                var name = GetPropertyName(p.Name);
                if (required || isNullable) {
                    return new { Type = type, Name = name, Required = required, hasValue = false, value = "" };
                }

                if (type.Contains("List")) {
                    return new { Type = type, Name = name, Required = false, hasValue = true, value = "const []" };
                }

                return new { Type = type + (_request.IsNullSafety ? "?" : ""), Name = name, Required = false, hasValue = false, value = "" };
            }).ToList();

            if (_request.IsSignalRHub) {
                props.Add(new { Type = "HubConnection", Name = "connection", Required = true, hasValue = true, value = "" });
            }

            foreach (var prop in props) {
                result += result.AddTab();
                result += $"final {prop.Type} {prop.Name};";
                result += result.AddNewLine();
            }

            result += result.AddTab();
            result += $"const {_request.Type.Name}(" + "{";
            result += string.Join(", ", props.Select(p => p.Required ? $"{(_request.IsNullSafety ? "" : " @")}required this.{p.Name}" : ($"this.{p.Name} " + (p.hasValue ? $"= {p.value}" : ""))));
            result += "});";

            return result;
        }

        private string GetMethod(MethodInfo method) {
            var result = "";
            var name = method.Name;
            if (method.IsStatic) {
                return result;
            }

            result += result.AddTab();
            result += GetTypeName(method.ReturnType);
            result += result.AddSpace();
            result += method.Name;
            result += "(";
            var args = new List<string>();
            foreach (var parameterInfo in method.GetParameters()) {
                result += GetTypeName(parameterInfo.ParameterType);
                result += result.AddSpace();
                result += GetPropertyName(parameterInfo.Name);
                args.Add(parameterInfo.Name);
                if (method.GetParameters().Last() != parameterInfo) {
                    result += ", ";
                }
            }
            result += ")";
            if (_request.IsSignalRHub) {
                var hasResult = method.ReturnType.GetGenericArguments().Any();
                result += $" {(hasResult ? "async " : "")}=> ";
                result += result.AddNewLine();
                result += result.AddTab(2);
                var resultFromJson = "";
                var typeConverter = "";
                if (hasResult) {
                    var typeGeneric = GetTypeName(method.ReturnType.GetGenericArguments().First());
                    if (!StandardTypes.Contains(typeGeneric)) {
                        resultFromJson = typeGeneric + ".fromJson(";
                        typeConverter = "as Map<String,dynamic>";
                    } else {
                        typeConverter = " as " + typeGeneric;
                    }
                }
                result += (hasResult ? $"{resultFromJson} await connection.invoke(" : "connection.send(methodName: ") + $"'{name}'";
                result += args.Any() ? $" ,args: [{ string.Join(", ", args)}]" : "";
                result += string.IsNullOrWhiteSpace(resultFromJson) ? $"){typeConverter};" : $"){typeConverter});";
            } else {
                result += " { }";
            }
            result += result.AddNewLine(2);

            return result;
        }

        private string GetJsonParser() {
            var from = "".AddTab();
            from += _request.Type.Name;
            from += ".fromJson(Map<String, dynamic> json):";
            from += from.AddNewLine();
            var to = "".AddTab();
            to += "Map<String, dynamic> toJson() => {";
            to += to.AddNewLine();
            foreach (var typeProperty in _request.Properties) {
                var name = GetPropertyName(typeProperty.Name);
                var type = GetTypeName(typeProperty.PropertyType);
                from += from.AddTab(2);
                to += to.AddTab(2);
                if (type.Contains("List")) {
                    var subType = GetSubType(type);
                    var isStandardTypes = StandardTypes.Contains(subType);
                    from += $" {name} =json['{name}'] == null ? <{subType}>[] : {type}.from(json['{name}'].map((x) =>";
                    from += isStandardTypes ? "x) as Iterable)" : $"{subType}.fromJson(x as Map<String, dynamic>)) as Iterable)";
                    to += $"'{name}': List<dynamic>.from({name}.map((x) =>";
                    to += isStandardTypes ? "x))" : " x.toJson()))";
                } else if (typeProperty.PropertyType.IsEnum) {
                    from += $" {name} = {type}.values[json['{name}'] as int]";
                    to += $"'{name}': {name}.index";
                } else {
                    from += $" {name} = json['{name}'] as {type}";
                    to += $"'{name}': {name}";
                }
                if (_request.Properties.Last() != typeProperty) {
                    from += ",";
                    to += ",";
                    from += from.AddNewLine();
                    to += to.AddNewLine();
                } else {
                    from += ";";
                    to += to.AddNewLine();
                    to += to.AddTab();
                    to += "};";
                }
            }
            var result = from;
            result += result.AddNewLine(2);
            result += to;
            result += result.AddNewLine();
            return result;
        }

        private string GetTypeName(Type type) {
            if (type == typeof(Task) && _request.IsSignalRHub) {
                return "void";
            }
            if (type.BaseType == typeof(Task) && type.GetGenericArguments().Any()) {
                return $"Future<{GetTypeName(type.GetGenericArguments().First())}>";
            }
            if (type.Name == "String") {
                return type.Name;
            }
            if (type.IsNullableType()) {
                return type.GetTypeOfNullable().ToDartType() + (_request.IsNullSafety ? "?" : "");
            }
            if (type.IsArray) {
                return $"List<{GetTypeName(type.GetElementType())}>";
            }
            if (type.IsListType() || type.IsEnumerableType()) {
                return $"List<{GetTypeName(type.GetGenericArguments().First())}>";
            }
            if (!KnownType.Contains(type) && !_typesToGenerate.Contains(type)) {
                if (!type.GetGenericArguments().Any()) {
                    _typesToGenerate.Add(type);
                }
            }
            return type.ToDartType();
        }

        public static string GetPropertyName(string name) => $"{name[0].ToString().ToLower()}{name[1..]}";

        public static string GetSubType(string type) => type[(type.IndexOf("<", StringComparison.Ordinal) + 1)..^1];

    }
}
