namespace SnapShop.Framework
{
    public class Env
    {
        public static string MustGet(string environmentName)
        {
            var variable = System.Environment.GetEnvironmentVariable(environmentName);
            if (variable == null) throw new ArgumentNullException($"{variable} missing from environment");
            return variable;
        }

    }
}
