﻿using System.ComponentModel.DataAnnotations;

namespace GymServer.Models
{
    public class RegisterModel
    {


        public int Id { get; set; }


        public string? Name { get; set; }

        public string? LastName { get; set; }

   
        public DateTime? BirthDay { get; set; }

    
        public string? Phone { get; set; }


        public string? Email { get; set; }


        public string? Gender { get; set; }


        public string? Password { get; set; }

    }
}
