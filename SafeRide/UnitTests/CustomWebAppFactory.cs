using Microsoft.AspNetCore.Mvc.Testing;

namespace SRUnitTests
{
    class CustomWebAppFactory : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public CustomWebAppFactory()
        {
        }

        CustomWebAppFactory(string environment = "Development")
        {
            _environment = environment;
        }
    }
}





         //output.WriteLine("\nActual coordinates queried for hazards with type {0} in radius:", hazardType);
            //foreach (KeyValuePair<double, double> kvp in actualHazards)
            //{
            //    output.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            //}

            // use to show exact query results when troubleshooting DAO method
            //  Dictionary<double, double> expectedAccidentHazards = new Dictionary<double, double>()
            //  {
            //      [-73.99015] = 40.732204,
            //      [-73.990042] = 40.733456,
            //      [-73.990014] = 40.732818,
            //      [-73.989869] = 40.732689,
            //      [-73.990042] = 40.732689,

            //  };
            ////  Assert.NotStrictEqual(expectedHazards, actual);
            //  Assert.Equal(expectedAccidentHazards, actualHazards);
