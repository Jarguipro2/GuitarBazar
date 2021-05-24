using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GuitarBazar.Models
{
    public enum ConditionType { Neuf , Usagé }
    public enum GuitarType { Classique, Acoustique, Électrique}

    public class Guitar
    {
        public int Id { get; set; }

        [Display(Name = "Vendeur"), Required(ErrorMessage = "Obligatoire")]
        public int SellerId { get; set; }

        [ForeignKey("SellerId")]
        [Display(Name = "Vendeur")]
        public virtual Seller Seller { get; set; }

        [Display(Name = "Vendu")]
        public bool Sold { get; set; }

        [Display(Name = "Fabriquant"), Required(ErrorMessage = "Obligatoire")]
        public string Maker { get; set; }

        [Display(Name = "Modèle"), Required(ErrorMessage = "Obligatoire")]
        public string Model { get; set; }

        [Display(Name = "Photo  "), Url(ErrorMessage = "Invalide")]
        public string ImageURL { get; set; }

        [Display(Name = "Rotation")]
        public bool RotateImage { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = "Obligatoire")]
        public string Description { get; set; }

        [Display(Name = "Année"), Range(1800,3000, ErrorMessage ="Invalide")]
        public int Year { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }

        [Display(Name = "État")]
        public ConditionType Condition { get; set; }

        [Display(Name = "Type")]
        public GuitarType GuitarType { get; set; }

        [Display(Name = "Prix")]
        public int Price { get; set; }
        
        public Guitar()
        {
            AddDate = DateTime.Now;
            Sold = false;
        }
    }

    public class Seller
    {
        public int Id { get; set; }
        [Display(Name = "Nom"), Required(ErrorMessage = "Obligatoire")]
        public string Name { get; set; } // doit etre unique
        [Display(Name = "Courriel"), EmailAddress(ErrorMessage = "Invalide"), Required(ErrorMessage = "Obligatoire")]
        public string Email { get; set; }
        [Display(Name = "Téléphone"), Required(ErrorMessage = "Obligatoire")]
        public string Phone { get; set; }
    }
}