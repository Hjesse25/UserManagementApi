public static class UserRepository
{
    private static List<User> users = new List<User>();
    private static int nextId = 1;

    public static List<User> GetAll() => users;

    public static User GetById(int id) => users.FirstOrDefault(u => u.Id == id);

    public static User Add(User user)
    {
        user.Id = nextId++;
        users.Add(user);
        return user;
    }

    public static bool Update(int id, User updatedUser)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        existing.Name = updatedUser.Name;
        existing.Email = updatedUser.Email;
        return true;
    }

    public static bool Delete(int id)
    {
        var user = GetById(id);
        if (user == null) return false;

        users.Remove(user);
        return true;
    }
}