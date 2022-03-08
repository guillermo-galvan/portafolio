using GG.SSO.Entity.Table.Sso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.SSO.BusinesLogic.Model.Identity
{
    public class ApplicationAreaRole
    {
        public Areas Area { get; set; }

        public Roles Role { get; set; }

        public ApplicationAreaRole()
        { }

        public ApplicationAreaRole(Roles roles, Areas areas)
        {
            Role = roles ?? throw new ArgumentNullException(nameof(roles));
            Area = areas ?? throw new ArgumentNullException(nameof(areas));
        }
    }
}
