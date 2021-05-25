using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class AddNewGame
    {
        [Required(ErrorMessage = "Required field")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters"), MaxLength(30)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
        public string SessionName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters"), MaxLength(30)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
        public string PlayerOne { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters"), MaxLength(30)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
        public string PlayerTwo { get; set; }

        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
        public string PlayerThree { get; set; }

        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
        public string PlayerFour { get; set; }
    }
}
