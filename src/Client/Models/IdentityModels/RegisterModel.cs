namespace Client.Models.IdentityModels {
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel {

        [Required(ErrorMessage = "Proszę podać nazwę użytkownika.")]
        [RegularExpression("^(?=.{5,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$",
            ErrorMessage = "Nazwa użytkownika powinna mieć długość od 5 do 20 znaków oraz nie może zawierać znaków specjalnych.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Proszę podać adres email.")]
        [EmailAddress(ErrorMessage = "Proszę podać poprawny adres email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podać nowe hasło.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\d a-zA-Z]).{8,20}$", ErrorMessage = "Hasło powinno zawierać od 8 do 20 znaków, jeden znak specjalny i dużą literę.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Proszę powtórzyć wcześniej podane hasło.")]
        [Compare("Password", ErrorMessage = "Podane hasła nie są jednakowe.")]
        public string RepeatPassword { get; set; }

        public bool TermsOfUseAgree { get; set; }
    }
}