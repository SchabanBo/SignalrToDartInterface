using System;

namespace SignalRToDartInterface {
    public class EnumGenerator {
        private readonly Type _type;

        public EnumGenerator(Type type) {
            if (!type.IsEnum) {
                throw new Exception($"Type {type.Name} is not enum");
            }
            _type = type;
        }

        public string Generate() {
            var result = $"enum {_type.Name}";
            result += "{";
            result += result.AddNewLine();
            foreach (var enumName in _type.GetEnumNames()) {
                result += result.AddTab();
                result += enumName;
                result += ",";
                result += result.AddNewLine();
            }
            result += "}";
            return result;
        }
    }
}
