namespace Frontend.Utility
{
    public class StaticDetails
    {
        public static string? EmployeeAPIBase {  get; set; }
        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
    }
}
