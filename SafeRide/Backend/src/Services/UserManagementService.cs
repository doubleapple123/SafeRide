using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;

namespace Backend.Services;

public class UserManagementService
{
    private IUserDAO _userDao;

    public UserManagementService(IUserDAO userDao)
    {
        _userDao = userDao;
    }

    public bool DeleteUser(string username)
    {
        return _userDao.Delete(username);
    }

    public bool DisableAccount(string username)
    {
        return _userDao.Disable(username);
    }

    public bool EnableAccount(string username)
    {
        return _userDao.Enable(username);
    }

}