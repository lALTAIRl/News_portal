using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections;


namespace News_portal.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public List<NewsApplicationUser> NewsApplicationUsers { get; set; }

    }   
}