using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace Backend.Services;

public class UserManagementService
{
    private IUserSecurityDAO _userDao;

    public UserManagementService(IUserSecurityDAO userDao)
    {
        _userDao = userDao;
    }

    public bool CreateUser(UserSecurityModel model)
    {
        return _userDao.Create(model);
    }

    public bool UpdateUser(string username, UserSecurityModel model)
    {
        return _userDao.Update(username, model);
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