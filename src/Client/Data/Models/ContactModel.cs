namespace Client.Data.Models {
    using System.ComponentModel.DataAnnotations;

    public class ContactModel {
        [Required]
        [EmailAddress(ErrorMessage = "Pole powinno zawierać poprawny adres email.")]
        public string Sender { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Długość tematu powinna mieścić się w zakresie od 4 do 35 znaków.",
            MinimumLength = 4)]
        public string Topic { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Długość tematu powinna mieścić się w zakresie od 10 do 500 znaków.",
            MinimumLength = 10)]
        public string Message { get; set; }
    }
}