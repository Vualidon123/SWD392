public static class AuthorizationPolicies
{
    /// <summary>
    /// This policy only requires token type Access + Access Authentication Scheme
    /// </summary>
    public const string Access = "Access";
    public const string Refresh = "Refresh";

    public const string Admin = "Admin";

    public const string Staff = "Staff";

    public const string Student = "Student";


    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Admin, policy =>
            {
                // TODO: change this into 1|2|3 if enum role
                policy.RequireRole("Admin");
                policy.AddAuthenticationSchemes(Access);
                policy.RequireClaim("TokenType", Access);
            })
            .AddPolicy(Refresh, policy =>
            {
                policy.AddAuthenticationSchemes(Refresh);
                policy.RequireClaim("TokenType", Refresh);
            })
            .AddPolicy(Access, policy =>
            {
                policy.AddAuthenticationSchemes(Access);
                policy.RequireClaim("TokenType", Access);
            })
            .AddPolicy(Staff, policy =>
            {
                policy.RequireRole("Staff");
                policy.AddAuthenticationSchemes(Access);
                policy.RequireClaim("TokenType", Access);
            })
            .AddPolicy(Student, policy =>
            {
                policy.RequireRole("Student");
                policy.AddAuthenticationSchemes(Access);
                policy.RequireClaim("TokenType", Access);
            });
    }
}
