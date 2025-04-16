namespace ProductAuthenticatorApp.Data
{
    public class DbInitializer
    {
        public async void SeedUserType(ApplicationDbContext dbContext)
        {
            try
            {
                dbContext.Database.EnsureCreated();

                if (!dbContext.UserTypes.Any())
                {
                    string[] Usertypes = { "Personal", "Organazation" };
                  

                    foreach (string usertype in Usertypes)
                    {
                        //dbContext.UserTypes.Add(Usertypes);
                        Console.WriteLine($"{usertype} Added");
                    }

                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"User Types Aready Exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured When Trying To Seed UserTypes!!!  : {ex.Message}");
            }
        }
    }
}
