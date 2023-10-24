namespace MyApi.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Customers = "Pages.Customers";
        public const string Pages_Users_Activation = "Pages.Users.Activation";
        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Informacoes = "Pages.Informacoes";
    }

    public static class AppPermissionNames
    {
        public const string App_Home = "App.Home";
    }

    public static class AdministrativePermissionActions
    {
        public const string Administrative_User_See = "Administrative.User.See";
        public const string Administrative_User_Edit = "Administrative.User.Edit";
        public const string Administrative_User_Get = "Administrative.User.Get";
        public const string Administrative_Customer_See = "Administrative.Customer.See";
        public const string Administrative_Customer_Edit = "Administrative.Customer.Edit";
        public const string Administrative_Informacao_See = "Administrative.Informacao.See";
        public const string Administrative_Informacao_Edit = "Administrative.Informacao.Edit";
    }

    public static class RoleNames
    {
        public const string RoleUser = "RoleUser";

    }
}

