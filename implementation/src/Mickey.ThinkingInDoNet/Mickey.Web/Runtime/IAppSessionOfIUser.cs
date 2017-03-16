namespace Mickey.Web.Runtime
{
    public interface IAppSession<IUser>
        where IUser : Microsoft.AspNet.Identity.IUser
    {
        IUser User { get; }
    }
}
