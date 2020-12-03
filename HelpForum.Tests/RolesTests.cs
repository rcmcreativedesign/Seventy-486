using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelpForum.Tests
{
    [TestClass]
    public class RolesTests
    {
        [TestMethod]
        public void Roles_RegisteredUser()
        {
            Roles.Enabled = true;
            var isUserInRole = Roles.IsUserInRole("username@email.com", "AuthenticatedUser");
            Assert.IsTrue(isUserInRole);
        }
    }
}
