using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRToDartInterface {
    public static class ConvertSignalRToDart {
        public static string Convert(List<GenerateRequest> requests) =>
            string.Join("".AddNewLine(3),
                requests.Select(r =>
                    r.Type.IsEnum ?
                        new EnumGenerator(r.Type).Generate() :
                        new InterfaceGenerator(r).Generate()));
    }
}
