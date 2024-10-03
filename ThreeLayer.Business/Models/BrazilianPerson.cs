﻿namespace ThreeLayer.Business.Models
{
    public class BrazilianPerson : Entity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public DateOnly BirthDate { get; set; }
    }
}
