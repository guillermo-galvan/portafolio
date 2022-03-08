using GG.SSO.Entity.Table.Sso;
using System.Collections.Generic;

namespace GG.SSO.BusinesLogic.Model.Identity
{
    public class ApplicationUser
    {
        public Users User { get; set; }

        public ICollection<ClientsBasic> Clients { get; set; }

        public ApplicationUser()
        { }

        public ApplicationUser(Users user, ICollection<ClientsBasic> clients)
        {
            User = user;
            Clients = clients;

        }
    }
}
