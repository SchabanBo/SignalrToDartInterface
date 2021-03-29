namespace SignalRToDartInterface.ExampleHub.Types {
    public class HubResult {
        public Result Result { get; set; }
        public string Message { get; set; }
    }

    public enum Result {
        Done,
        Error
    }
}
