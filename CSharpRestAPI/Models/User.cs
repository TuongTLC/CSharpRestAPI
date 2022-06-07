using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSharpRestAPI
{
    public partial class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public bool Status { get; set; }
        public int Id { get; set; }
    }
}
