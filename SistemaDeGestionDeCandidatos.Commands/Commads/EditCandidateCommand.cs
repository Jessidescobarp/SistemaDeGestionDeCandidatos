﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestionDeCandidatos.Commands.Commads
{
    public class EditCandidateCommand
    {
            public int IdCandidate { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime Birthdate { get; set; }
            public string Email { get; set; }

    }
}
