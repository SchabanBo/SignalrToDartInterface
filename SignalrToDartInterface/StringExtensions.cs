using System;

namespace SignalRToDartInterface {
    public static class StringExtensions {

        public static string AddSpace(this string value) {
            return $" ";
        }

        public static string AddTab(this string value, int count = 1, int tabLength = 3) {
            return " ".PadRight(count * tabLength);
        }

        public static string AddNewLine(this string value, int count = 1) {
            var result = "";
            for (int i = 0; i < count; i++) {
                result += Environment.NewLine;
            }
            return result;
        }
    }
}
