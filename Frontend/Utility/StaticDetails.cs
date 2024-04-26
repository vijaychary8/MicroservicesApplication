namespace Frontend.Utility
{
    public class StaticDetails
    {
        public static string? EmployeeAPIBase {  get; set; }
        public static string? AuthApiBase { get; set; }

        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
