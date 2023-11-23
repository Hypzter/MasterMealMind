namespace MasterMealMind.API.DAL
{
    public class Connection
    {
        private readonly string _connectionString = "Server=.\\SQLExpress;Database=MasterMealMind;Trusted_Connection=True;Encrypt=false";

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
