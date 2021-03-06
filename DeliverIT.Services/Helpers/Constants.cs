namespace DeliverIT.Services.Helpers
{
    public static class Constants
    {
        public const string DOMAIN_NAME = "https://localhost:5002";
        public const string SESSION_ROLE_KEY = "CurrentRole";
        public const string SESSION_ID_KEY = "CurrentId";
        public const string SESSION_AUTH_KEY = "CurrentUser";

        public const string HEADER_AUTH_KEY = "Authorization";

        public const string ROLE_EMPLOYEE = "Admin";
        public const string ROLE_USER = "User";

        public const string NOT_LOGGED = "You are not logged!";
        public const string NOT_AUTHORIZED = "You are not authorized!";
        public const string WRONG_CREDENTIALS = "Wrong credentials!";

        public const string LOGGED = "You logged successfully!";
        public const string REQUIRED = "This field is required!";
        public const string ACCOUNT_NOT_FOUND = "Account not found!";
        public const string WRONG_ID = "Wrong id!";
        public const string INCORRECT_DATA = "Incorrect or missing data!";

        public const string COUNTRY_NOT_FOUND = "Country not found!";
        public const string SHIPMENT_NOT_FOUND = "Shipment not found!";
        public const string PARCEL_NOT_FOUND = "Parcel not found!";
        public const string SHIPMENT_ALREADY_ARRIVED = "Shipment already arrived!";
        public const string CITY_NOT_FOUND = "City not found";
        public const string CUSTOMER_NOT_FOUND = "Customer not found";
        public const string EMPLOYEE_NOT_FOUND = "Employee not found";
        public const string WAREHOUSE_NOT_FOUND = "Warehouse not found";

        public const string INVALID_ID = "Invalid Id";
        public const string CUSTOMER_EXISTS = "Customer with this email already exists!";
        public const string EMPLOYEE_EXISTS = "Employee with this email already exists!";
        public const string EMAIL_EXISTS = "This email address is already taken";
        public const string CITY_EXISTS = "City with this name already exists!";
        public const string COUNTRY_EXISTS = "Country with this name already exists!";
        public const string WAREHOUSE_ADDRESS_EXISTS = "Warehouse with this address already exists!";
    }
}
